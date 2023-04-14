using Foxic_Backend_Project_.DAL;
using Foxic_Backend_Project_.Entities;
using Foxic_Backend_Project_.Utilites.Extensions;
using Foxic_Backend_Project_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic_Backend_Project_.Areas.FoxicArea.Controllers
{
    [Area("FoxicArea")]
    public class ProductsController : Controller
    {
        private readonly FoxicDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(FoxicDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _context.Products.Include(p => p.ProductSizeColors).ThenInclude(p => p.Size)
                                                       .Include(p => p.ProductSizeColors).ThenInclude(p => p.Color)
                                                        .Include(p => p.ProductImages)
                                                        .Include(p=>p.Collection)
                                                        .AsNoTracking()
                                                        .AsEnumerable();
            return View(products);
        }

        public IActionResult Create()
        {
            AllViewBagsData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM newProduct)
        {
            AllViewBagsData();
            TempData["InvalidImages"] = string.Empty;
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!newProduct.MainPhoto.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");
                return View();
            }
            if (!newProduct.MainPhoto.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 1MB");
                return View();
            }

            Product product = new()
            {
                Name = newProduct.Name,
                ShortDesc = newProduct.ShortDesc,
                LongDesc = newProduct.LongDesc,
                Price = newProduct.Price,
                GlobalTabId = newProduct.GlobalTabId,
                CollectionId = newProduct.CollectionId
            };
            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
            foreach (var image in newProduct.Images)
            {
                if (!image.IsValidFile("image/") || !image.IsValidLength(1))
                {
                    TempData["InvalidImages"] += image.FileName;
                    continue;
                }
                ProductImage productImage = new()
                {
                    IsMain = false,
                    Path = await image.CreateImage(imageFolderPath, "products")
                };
                product.ProductImages.Add(productImage);
            }


            ProductImage main = new()
            {
                IsMain = true,
                Path = await newProduct.MainPhoto.CreateImage(imageFolderPath, "products")
            };
            product.ProductImages.Add(main);

            foreach (int id in newProduct.CategoryIds)
            {
                ProductCategory category = new()
                {
                    CategoryId = id
                };
                product.ProductCategories.Add(category);
            }
            foreach (int id in newProduct.TagIds)
            {
                ProductTag tag = new()
                {
                    TagId = id
                };
                product.ProductTags.Add(tag);
            }
           

            //Create ColorSizePlant
            ColorSizeProduct(product, newProduct);

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return BadRequest();
            ProductVM? model = EditedModel(id);
            AllViewBagsData();
            if (model is null) return BadRequest();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductVM editedProduct)
        {
            AllViewBagsData();
            ProductVM? model = EditedModel(id);

            Product? product = await _context.Products.Include(p => p.ProductCategories)
                                                .Include(p => p.ProductTags)
                                                .Include(p => p.ProductImages)
                                                .Include(p=>p.Collection)
                                                .FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return BadRequest();

            IEnumerable<string> removables = product.ProductImages.Where(p => !editedProduct.ImagesId.Contains(p.Id)).Select(i => i.Path).AsEnumerable();
            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
            foreach (string removable in removables)
            {
                string path = Path.Combine(imageFolderPath, "products", removable);
                await Console.Out.WriteLineAsync(path);
                Console.WriteLine(FileUpload.DeleteImage(path));
            }


            if (editedProduct.MainPhoto is not null)
            {
                if (!editedProduct.MainPhoto.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image file");
                    return View();
                }
                if (!editedProduct.MainPhoto.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 2MB");
                    return View();
                }
                await AdjustPlantPhoto(true, editedProduct.MainPhoto, product);
            }

            product.ProductImages.RemoveAll(p => !editedProduct.ImagesId.Contains(p.Id));
            if (editedProduct.Images is not null)
            {
                foreach (var images in editedProduct.Images)
                {
                    if (!images.IsValidFile("image/") || !images.IsValidLength(2))
                    {
                        TempData["NonSelect"] += images.FileName;
                        continue;
                    }
                    ProductImage productImage = new()
                    {
                        IsMain = false,
                        Path = await images.CreateImage(imageFolderPath, "products")
                    };
                    product.ProductImages.Add(productImage);
                }


            }
            product.Name = editedProduct.Name;
            product.Price = editedProduct.Price;
            product.ShortDesc = editedProduct.ShortDesc;
            product.LongDesc = editedProduct.LongDesc;
            product.DiscountPrice = editedProduct.DiscountPrice;
            


            //TODO Edit Category and Tag IDs
            product.ProductCategories.Clear();
            product.ProductTags.Clear();

            EditCategories(product, editedProduct);
            EditTags(product, editedProduct);

            //TO DO ColorsizePlant
            ColorSizeProduct(product, editedProduct);
            DeleteColorSize(product, editedProduct);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// I hid the viewbags I sent in actions here
        /// </summary>
        private void AllViewBagsData()
        {
            ViewBag.Informations = _context.GlobalTabs.AsEnumerable();
            ViewBag.Collections = _context.Collections.AsEnumerable();
            ViewBag.Categories = _context.Categories.AsEnumerable();
            ViewBag.Tags = _context.Tags.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Colors.AsEnumerable();
        }

        /// <summary>
        /// Retrieves a PlantVM model with the given ID from the database, populating its properties and collections with data from related entities, and returns it.
        /// </summary>
        /// <param name="id">The ID of the plant to retrieve.</param>
        /// <returns>A PlantVM object populated with data from the database, or null if the plant is not found.</returns>
        private ProductVM? EditedModel(int id)
        {
            ProductVM? model = _context.Products.
                    Include(pc => pc.ProductCategories).
                        Include(pt => pt.ProductTags).
                        Include(pt => pt.ProductSizeColors).
                            Select(p => new ProductVM
                            {
                                Id = p.Id,
                                Name = p.Name,
                                ShortDesc = p.ShortDesc,
                                Price = p.Price,
                                DiscountPrice = p.DiscountPrice,
                                GlobalTabId = p.GlobalTabId,
                                ProductSizeColor = p.ProductSizeColors,
                                CategoryIds = p.ProductCategories.Select(p => p.CategoryId).ToList(),
                                TagIds = p.ProductTags.Select(p => p.TagId).ToList(),
                                AllImages = p.ProductImages.Select(i => new ProductImage
                                {
                                    Id = i.Id,
                                    IsMain = i.IsMain,
                                    Path = i.Path
                                }).ToList()
                            }).FirstOrDefault(p => p.Id == id);
            return model;
        }

        /// <summary>
        /// Adjusts the photo of a plant by deleting the existing image and replacing it with a new one.
        /// </summary>
        /// <param name="ismain">A boolean value indicating whether the selected image is the main photo of the plant.</param>
        /// <param name="image">The image to replace the existing photo.</param>
        /// <param name="plant">The plant object whose photo needs to be adjusted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task AdjustPlantPhoto(bool? Ismain, IFormFile image, Product product)
        {
            string imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
            string filePath = Path.Combine(imagefolderPath, "products", product.ProductImages.FirstOrDefault(p => p.IsMain == Ismain).Path);
            FileUpload.DeleteImage(filePath);
            product.ProductImages.FirstOrDefault(p => p.IsMain == Ismain).Path = await image.CreateImage(imagefolderPath, "products");
        }

        /// <summary>
        /// to change tags
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="edited"></param>
        private void EditCategories(Product product, ProductVM editedProduct)
        {
            foreach (int categoryId in editedProduct.CategoryIds)
            {
                Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (category != null)
                {
                    ProductCategory productCategory = new()
                    {
                        Category = category
                    };
                    product.ProductCategories.Add(productCategory);
                }
            }

        }

        /// <summary>
        /// to change categorys
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="edited"></param>
        private void EditTags(Product product, ProductVM editedProduct)
        {
            foreach (int tId in editedProduct.TagIds)
            {
                Tag? tag = _context.Tags.FirstOrDefault(t => t.Id == tId);
                if (tag != null)
                {
                    ProductTag productTag = new()
                    {
                        Tag = tag
                    };
                    product.ProductTags.Add(productTag);

                }
            }
        }


        /// <summary>
        /// to create new plant size color. and if the same color plant create, in order not to create it again.  
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="edited"></param>
        private void ColorSizeProduct(Product product, ProductVM editedProduct)
        {
            if (editedProduct.ColorsSizesQuantity != null)
            {
                string[] colorSizeQuantities = editedProduct.ColorsSizesQuantity.Split(',');
                foreach (string ColorsSizesQuantity in colorSizeQuantities)
                {
                    string[] datas = ColorsSizesQuantity.Split('-');
                    ProductSizeColor productSizeColor = new()
                    {
                        SizeId = int.Parse(datas[0]),
                        ColorId = int.Parse(datas[1]),
                        Quantity = int.Parse(datas[2])
                    };

                    var repeatCs = product.ProductSizeColors.FirstOrDefault(p => p.SizeId == productSizeColor.SizeId && p.ColorId == productSizeColor.ColorId);
                    if (repeatCs != null)
                    {
                        repeatCs.Quantity = productSizeColor.Quantity;
                    }
                    else
                    {
                        product.ProductSizeColors.Add(productSizeColor);
                    }
                }
            }

        }


        /// <summary>
        /// to delete the color size plant
        /// </summary>
        /// <param name="plant"></param>
        /// <param name="edited"></param>
        /// <returns></returns>
        private async Task DeleteColorSize(Product product, ProductVM editedProduct)
        {
            if (editedProduct.DeleteColorSize != null)
            {
                string[] Ids = editedProduct.DeleteColorSize.Split(',');
                foreach (string productSizeColorId in Ids)
                {
                    int productCSId = int.Parse(productSizeColorId);
                    ProductSizeColor? productSizeColor = await _context.ProductSizeColors.FindAsync(productCSId);
                    if (productSizeColor != null)
                    {
                        product.ProductSizeColors.Remove(productSizeColor);
                    }
                }
            }

        }
    }
}
