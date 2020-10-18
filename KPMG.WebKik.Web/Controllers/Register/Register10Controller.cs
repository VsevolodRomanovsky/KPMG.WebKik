using System;
using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;
using System.Threading.Tasks;
using KPMG.WebKik.Services.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register10")]
    public class Register10Controller : ApiController
    {
        public Register10Controller(IRegister10Service service)
        {
        }

        [HttpGet, Route("{id:int}")]
        public virtual Register10ViewModel GetById(int id)
        {
            Register10Service service = new Register10Service();
            var result = service.GetRegister10(id);
            return Mapper.Map<Register10ViewModel>(result);
        }

        [HttpPost, Route("")]
        public Register10ViewModel Create([FromBody]Register10ViewModel register)
        {
            Register10Service service = new Register10Service();
            var entity = Mapper.Map<Register10>(register);
            var result = service.Create(entity);
            return Mapper.Map<Register10ViewModel>(result);
        }


       [HttpPost, Route("createRegisterData")]
		public virtual void CreateRegisterData([FromBody]Register10DataViewModel register)
        {

            Register10Service service = new Register10Service();
            var entity = Mapper.Map<Register10Data>(register);
            var result = service.CreateRegisterData(entity);
            return;
        }

        [HttpPost, Route("deleteRegisterData")]
        public virtual void DeleteRegisterData([FromBody]Register10DataViewModel register)
        {
            Register10Service service = new Register10Service();
            //var entity = Mapper.Map<Register9Data>(register);
            var result = service.DeleteRegisterData(register.Id);
            return;
        }

        [HttpPost, Route("editRegisterData")]
        public virtual void EditRegisterData([FromBody]Register10DataViewModel register)
        {
            Register10Service service = new Register10Service();
            var entity = Mapper.Map<Register10Data>(register);
            var result = service.EditRegisterData(entity);
            return;
        }


        [HttpPost, Route("edit")]
        public virtual void Edit([FromBody]Register10ViewModel register)
        {
            //throw new NotImplementedException();
        }

        [HttpPost, Route("calculate")]
        public Register10ViewModel Calculate([FromBody]Register10ViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}