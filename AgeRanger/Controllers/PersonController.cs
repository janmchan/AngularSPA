using AgeRanger.Core.Contracts.Logging;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.Core.Contracts.Messages;
using AgeRanger.Extensions;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Contracts.Repositories;
using AgeRanger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AgeRanger.Controllers
{
    /// <summary>
    /// Provides API for managing Person object from Database
    /// </summary>
    [Route("api/[controller]")]
    public class PersonController: System.Web.Http.ApiController
    {
        private readonly IPersonRepository _personRepository;
        private readonly IEnumerable<IAgeGroup> _ageGroups;
        private readonly ILogger _logger;
        private readonly IMapper<PersonModel, IPerson> _mapper;
        private IMessageFinder _message;
        public PersonController(IPersonRepository personRepository,
                                IAgeGroupRepository ageGroupRepository,
                                ILogger logger,
                                IMessageFinder message,
                                IMapper<PersonModel, IPerson> mapper)
        {
            _personRepository = personRepository;
            _logger = logger;
            _message = message;
            _mapper = mapper;
            try
            {
				_ageGroups = ageGroupRepository.Find().Result;
			}
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                throw;
            }
           
            
        }
        public async Task<IEnumerable<PersonModel>> Get()
        {
            try
            {
                var people = await _personRepository.Find();
                var peopleWithAge = people.Select(p => new PersonModel(p, _ageGroups.AsQueryable()));
                return peopleWithAge;
            }
            catch (Exception ex)
            {
				var message = _message.Find(MessageKey.GetUserFail);
				var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				response.Content = new StringContent(message);
				_logger.Error(ex.ToString());
				throw new System.Web.Http.HttpResponseException(response);
            }

        }
        public async Task<PersonModel> Get(int id)
        {
            try
            {
                var person = await _personRepository.FindById(id);
                if (id == 0 || person.Id == 0)
                {
                    throw new ArgumentException(string.Format(_message.Find(MessageKey.PersonDoesNotExist), id));
                }
                var peopleWithAge = new PersonModel(person, _ageGroups.AsQueryable());
                return peopleWithAge;
            }
            catch (Exception ex)
            {
				var message = _message.Find(MessageKey.GetUserFail);
				var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				response.Content = new StringContent(message);
				_logger.Error(ex.ToString());
				throw new System.Web.Http.HttpResponseException(response);
			}

        }

        [HttpPost]
        public async Task<ResponseModel> Post(PersonModel model)
        {
			//TODO: Integrate
			
            var errorMsg = _message.Find(MessageKey.AddUserFail);
            try
            {   //This can be placed in a separate validator class 
				
                if (!ModelState.IsValid)
                {
                    return new ResponseModel(false, errorMsg);
                }
                var dataModel = _mapper.Map(model);

				dataModel.TaskName = "CreatePerson";
				dataModel.TaskId = Guid.NewGuid();

				var personId = await _personRepository.Add(dataModel);
                if (personId != 0)
                {
                    return new ResponseModel(true);
                }
                
                _logger.Error(errorMsg);
                return new ResponseModel(false, errorMsg);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return new ResponseModel(false, _message.Find(MessageKey.AddUserFail));
                
            }
        }

        [HttpPut]
        public async Task<ResponseModel> Put(PersonModel model)
        {
            try
            {
                //This can be placed in a separate validator class 
                if (!ModelState.IsValid || model.Id == 0)
                {
                    return new ResponseModel(false, _message.Find(MessageKey.PutUserFail));
                }
                var existingPerson = await _personRepository.FindById(model.Id);
                if (existingPerson.Id == 0) //Default
                {
                    throw new ArgumentException(string.Concat(_message.Find(MessageKey.PersonDoesNotExist), model.Id));
                }
                existingPerson.FirstName = model.FirstName;
                existingPerson.LastName = model.LastName;
                existingPerson.Age = model.Age;
				existingPerson.TaskName = "EditPerson";
				existingPerson.TaskId = Guid.NewGuid();
				await _personRepository.Set(existingPerson);
                return new ResponseModel(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return new ResponseModel(false, _message.Find(MessageKey.PutUserFail));
             }

            
         }
    }
}