using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Concrete.Managers;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.ComplexTypes;
using Kuzgun.Entities.ComplexTypes.CategoriesDTO;
using Kuzgun.Entities.ComplexTypes.RolesDTO;
using Kuzgun.Entities.ComplexTypes.SubCategoriesDTO;
using Kuzgun.Entities.ComplexTypes.UsersDTO;
using Kuzgun.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private ICategoryService _categoryService;
        private ISubCategoryService _subCategoryService;
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;


        public AdminsController(ICategoryService categoryService, ISubCategoryService subCategoryService, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        [HttpGet]
        [Route("getCategories")]

        public IActionResult GetCategories()
        {

            var categories = _categoryService.GetAll();
            //var result = _mapper.Map<IEnumerable<CategoryForReturnDTO>>(categories);
            if (categories != null)
            {
                return Ok(categories);
            }

            return BadRequest("Kategoriler Getirilemedi");

        }
        
        [HttpPost]
        [Route("createCategory")]
        public IActionResult CreateCategory(CategoryForCreationDTO model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("Eksik ya da hatalı bilgi girdiniz");
            //}

            var category = new Category
            {
                CategoryName = model.CategoryName,
                DateCreated = DateTime.Now,
                IsDeleted = false


            };
            _categoryService.Create(category);
            return Ok();

        }

        [HttpGet]
        [Route("getCategory/{id}")]
        public IActionResult GetCategory(int id)
        {

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return BadRequest("Kategori bulunamadı");
            }

            //var result = _mapper.Map<CategoryForReturnDTO>(category);

            return Ok(category);

        }

        [HttpPut]
        [Route("updateCategory/{id}")]
        public IActionResult UpdateCategory(CategoryForUpdateDTO model, int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("Eksik veya hatalı bilgi gönderdiniz");
            //}
            var category = _categoryService.GetById(id);
            category.Data.CategoryName = model.CategoryName;


            _categoryService.Update(category.Data);
            return Ok();

        }

        [HttpDelete]
        [Route("deleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);
            category.Data.IsDeleted = true;
            if (category != null)
            {
                _categoryService.Update(category.Data);
                return Ok("Kategori pasif hale getirildi");
            };

            return BadRequest("Kategori Bulunamadı");

        }
        [HttpPut]
        [Route("reviveCategory/{id}")]
        public IActionResult ReviveCategory(int id)
        {
            var category = _categoryService.GetById(id);
            category.Data.IsDeleted = false;
            if (category != null)
            {
                _categoryService.Update(category.Data);
                return Ok("Kategori aktif hale getirildi");
            };

            return BadRequest("Kategori Bulunamadı");

        }

        [HttpGet]
        [Route("getCategories/{id}/GetSubCategories")]
        public IActionResult GetSubCategories(int id)
        {

            var subcategories = _subCategoryService.GetAll().Where(s => s.CategoryId == id);
            if (subcategories != null)
            {
                //var result = _mapper.Map<IEnumerable<SubCategoryForReturnDTO>>(subcategories);
                return Ok(subcategories);
            };
            return BadRequest("Alt kategori bulunamadı");

        }

        [HttpPost]
        [Route("getCategories/{id}/CreateSubCategory")]
        public IActionResult CreateSubCategory(SubCategoryForCreationDTO model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Eksik yada hatalı bilgi girdiniz");
            }
            var subCategory = new SubCategory
            {
                SubCategoryName = model.SubCategoryName,
                CategoryId = id

            };

            _subCategoryService.Create(subCategory);
            return Ok();

        }


        [HttpGet]
        [Route("getSubCategory/{id}")]
        public IActionResult GetSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            if (subCategory == null)
            {
                return BadRequest("Kategori bulunamadı");
            }
            //var result = _mapper.Map<SubCategoryForReturnDTO>(subCategory);
            return Ok(subCategory);

        }

        [HttpPut]
        [Route("updateSubCategory/{id}")]
        public IActionResult UpdateSubCategory(SubCategoryForUpdateDTO model, int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            if (subCategory == null)
            {
                return BadRequest("Alt kategori bulunamadı");
            }
            subCategory.SubCategoryName = model.SubCategoryName;
            if (ModelState.IsValid)
            {
                _subCategoryService.Update(subCategory);
                return Ok();
            }
            return BadRequest("Alt kategori güncellenemedi");

        }

        [HttpDelete]
        [Route("deleteSubCategory/{id}")]
        public IActionResult DeleteSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            subCategory.IsDeleted = true;

            if (subCategory != null)
            {
                _subCategoryService.Update(subCategory);
                return Ok("Kategori pasif hale getirildi");
            };

            return BadRequest("Kategori Bulunamadı");

        }

        [HttpPut]
        [Route("reviveSubCategory/{id}")]
        public IActionResult ReviveSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetById(id);
            subCategory.IsDeleted = false;

            if (subCategory != null)
            {
                _subCategoryService.Update(subCategory);
                return Ok("Kategori aktif hale getirildi");
            };

            return BadRequest("Kategori Bulunamadı");

        }


        [HttpGet]
        [Route("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null)
            {
                return BadRequest("Roller Bulunamadı");
            }

            return Ok(roles);
        }

        [HttpPost]
        [Route("addRole")]
        public async Task<IActionResult> AddRole(RoleForCreationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Eksik veya hatalı bilgi girdiniz");
            }
            Role role = new Role();
            role.Name = model.Name.ToLower();

            var roleAdd = await _roleManager.CreateAsync(role);
            if (!roleAdd.Succeeded)
            {
                return BadRequest("Rol oluşturulamadı");
            }
            return Ok();


        }

        [HttpPut]
        [Route("updateRole/{id}")]

        public async Task<IActionResult> UpdateRole(int id, RoleForUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest("Eksisk veya hatalı bilgi girdiniz");
            }
            var role = await _roleManager.FindByIdAsync(id.ToString());
            role.Name = model.Name;
            await _roleManager.UpdateAsync(role);
            return Ok("Rol başarıyla güncellendi");

        }


        [HttpDelete]
        [Route("deleteRole/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                if (role == null)
                {
                    return BadRequest("Rol Bulunamadı");
                }

                var result = await _roleManager.DeleteAsync(role);

                return Ok();
            }
            return BadRequest("Rol bilgisi alınamadı");

        }

        [HttpPut]
        [Route("changeUserRole/{userId}")]
        public async Task<IActionResult> ChangeUserRole(int userId, UserForChangeRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Rol ismi alınamadı");


            var user = await _userManager.FindByIdAsync(userId.ToString());

            var userRoles = await _userManager.GetRolesAsync(user);
            try
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, model.Name);

            }
            catch (Exception)
            {

                throw;
            }

            return Ok();
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUserList()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                return BadRequest("Kullanıcılar Getirilemedi");
            }
            //var result = _mapper.Map<IEnumerable<UserForDetailDTO>>(users);
            return Ok(users);
        }

        [HttpGet]
        [Route("getUserDetail/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                var userRole = await _userManager.GetRolesAsync(user);
                //var result = _mapper.Map<UserForDetailDTO>(user);
                //result.UserRole = userRole.FirstOrDefault();

                return Ok(user);
            }

            return BadRequest("Kullanıcı Detayları getirilemedi");
        }



        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id > 0)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return BadRequest("Kullanıcı Bulunamadı");
                }
                user.IsDeleted = true;
                await _userManager.UpdateAsync(user);
                return Ok();
            }

            return BadRequest();

        }

        [HttpPut]
        [Route("reviveUser/{id}")]
        public async Task<IActionResult> ReviveUser(int id)
        {
            if (id > 0)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return BadRequest("Kullanıcı Bulunamadı");
                }
                user.IsDeleted = false;
                await _userManager.UpdateAsync(user);
                return Ok();
            }

            return BadRequest();

        }
















    }
}
