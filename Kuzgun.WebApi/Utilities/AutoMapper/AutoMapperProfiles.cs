using AutoMapper;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.ComplexTypes.CategoriesDTO;
using Kuzgun.Entities.ComplexTypes.PostCommentsDTO;
using Kuzgun.Entities.ComplexTypes.PostsDTO;
using Kuzgun.Entities.ComplexTypes.SubCategoriesDTO;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.WebApi.Utilities.AutoMapper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForChangeEmailDTO>();
            CreateMap<PostComment, CommentForReturnDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(i => i.User.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(i => i.UserId))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(i => i.User.ImageUrl));
            CreateMap<User, UserForDetailDTO>();
            CreateMap<Category, CategoryForReturnDTO>();
            CreateMap<SubCategory, SubCategoryForReturnDTO>();
            CreateMap<Post, PostForReturnDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(i => i.User.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(i => i.Category.CategoryName))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(i => i.SubCategory.SubCategoryName))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(i => i.User.ImageUrl));

            CreateMap<SubCategory, SubCategoryForReturnDTO>();
        }
    }
}
