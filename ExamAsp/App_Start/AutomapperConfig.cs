using AutoMapper;
using BusinessLayer.BModel;
using DataLayer.Entities;
using ExamAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace ExamAsp.App_Start
{
    public static class AutomapperConfig
    {
        public static void RegisterWithUnity(IUnityContainer container)
        {
            IMapper mapper = CreateMapperConfig().CreateMapper();

            container.RegisterInstance(mapper);
        }


        static MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                    cfg.CreateMap<Authors, AuthorBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                    .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                    cfg.CreateMap<AuthorBO, AuthorModel>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorModel>());

                    cfg.CreateMap<AuthorModel, AuthorBO>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                    cfg.CreateMap<AuthorBO, Authors>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<Authors>());


                    cfg.CreateMap<Books, BookBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                    .ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                    cfg.CreateMap<BookBO, BookModel>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<BookModel>());

                    cfg.CreateMap<BookModel, BookBO>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                    cfg.CreateMap<BookBO, Books>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<Books>());


                    cfg.CreateMap<Users, UserBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                    .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                    cfg.CreateMap<UserBO, UserModel>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<UserModel>());

                    cfg.CreateMap<UserModel, UserBO>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<UserBO>());

                    cfg.CreateMap<UserBO, Users>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<Users>());



                    cfg.CreateMap<Genres, GenreBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                    .ConstructUsing(item => DependencyResolver.Current.GetService<GenreBO>());

                    cfg.CreateMap<GenreBO, GenreModel>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<GenreModel>());

                    cfg.CreateMap<GenreModel, GenreBO>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<GenreBO>());

                    cfg.CreateMap<GenreBO, Genres>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<Genres>());



                    cfg.CreateMap<Rating, RatingBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                    .ConstructUsing(item => DependencyResolver.Current.GetService<RatingBO>());

                    cfg.CreateMap<RatingBO, RatingModel>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<RatingModel>());

                    cfg.CreateMap<RatingModel, RatingBO>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<RatingBO>());

                    cfg.CreateMap<RatingBO, Rating>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<Rating>());



                    cfg.CreateMap<Comments, CommentBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                  .ConstructUsing(item => DependencyResolver.Current.GetService<CommentBO>());

                    cfg.CreateMap<CommentBO, CommentModel>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<CommentModel>());

                    cfg.CreateMap<CommentModel, CommentBO>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<CommentBO>());

                    cfg.CreateMap<CommentBO, Comments>()
                    .ConstructUsing(item => DependencyResolver.Current.GetService<Comments>());
                }
            );
        }
    }
}