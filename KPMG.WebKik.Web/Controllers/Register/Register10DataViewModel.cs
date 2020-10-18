using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using AutoMapper.Configuration;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using Newtonsoft.Json;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    public class Register10DataViewModel
    {

        public Register10ViewModel Register10 { get; set; }

        public int Id { get; set; }

        public int Register10Id { get; set; }
        //разделы
        public int SectionId { get; set; }

        public string AssetType { get; set; }

        public string CauseDisposal { get; set; }



        public decimal? Num1 { get; set; }


        public decimal? Num2 { get; set; }


        public decimal? Num3 { get; set; }


        public decimal? Num4 { get; set; }



        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Models.Registers.Register10Data, Register10DataViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Register10Id, o => o.MapFrom(s => s.Register10Id))
                .ForMember(d => d.AssetType, o => o.MapFrom(s => s.AssetType))
                .ForMember(d => d.CauseDisposal, o => o.MapFrom(s => s.CauseDisposal))
                .ForMember(d => d.Num1, o => o.MapFrom(s => s.Num1))
                .ForMember(d => d.Num2, o => o.MapFrom(s => s.Num2))
                .ForMember(d => d.Num3, o => o.MapFrom(s => s.Num3))
                .ForMember(d => d.Num4, o => o.MapFrom(s => s.Num4))
                .ForMember(d => d.SectionId, o => o.MapFrom(s => s.SectionId))
                .ForAllOtherMembers(x => x.Ignore());

            cfg.CreateMap<Register10DataViewModel, Models.Registers.Register10Data>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Register10Id, o => o.MapFrom(s => s.Register10Id))
                .ForMember(d => d.AssetType, o => o.MapFrom(s => s.AssetType))
                .ForMember(d => d.CauseDisposal, o => o.MapFrom(s => s.CauseDisposal))
                .ForMember(d => d.Num1, o => o.MapFrom(s => s.Num1))
                .ForMember(d => d.Num2, o => o.MapFrom(s => s.Num2))
                .ForMember(d => d.Num3, o => o.MapFrom(s => s.Num3))
                .ForMember(d => d.Num4, o => o.MapFrom(s => s.Num4))
                .ForMember(d => d.SectionId, o => o.MapFrom(s => s.SectionId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}