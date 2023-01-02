using AutoMapper;
using AutomataEHR_Repo.Interfaces;
using AutomataEHR_Services.Interfaces;
using AutomataEHR_Services.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutomataEHR_Services.Services
{
    public class BaseService<TEntity, T> : IBaseService<TEntity, T> where TEntity : class
        where T : BaseDto
    {
        // public AuthenticateResponse User => GetLoggedInUser.GetUserDetail();
        public readonly IBaseResponse<object> _baseResponse;
        public readonly IMapper _mapper;
        public readonly IGenericRepository<TEntity> _baseRepository;
        public BaseService(IBaseResponse<object> baseResponse,
            IMapper mapper,
            IGenericRepository<TEntity> baseRepository)
        {
            _baseResponse = baseResponse;
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public virtual async Task<IBaseResult<object>> Get(int id)
        {
            try
            {
                var result = _baseRepository.GetById(id);
                if (result != null)
                    return await _baseResponse.Success(result);
                else
                    return await _baseResponse.CustomErrorMessage("Record not found", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }



        public virtual async Task<IBaseResult<object>> GetAll()
        {
            try
            {
                var result = _baseRepository.GetAll();
                if (result != null)
                    return await _baseResponse.Success(result);
                else
                    return await _baseResponse.CustomErrorMessage("Record not found", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }


        public virtual async Task<IBaseResult<object>> Add(T dto)
        {
            try
            {
                // map model to new account object
                var mappedObject = _mapper.Map<TEntity>(dto);



                _baseRepository.Add(mappedObject);

                return await _baseResponse.Success(dto);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }
        public virtual async Task<IBaseResult<object>> Update(T dto, int id)
        {
            try
            {
                var getObject = _baseRepository.GetById(id);
                if (getObject == null)
                    return await _baseResponse.CustomErrorMessage("Failed to retrieve record for update.", HttpStatusCode.BadRequest);


                var result = _baseRepository.Update(getObject);
                if (result != null)
                    return await _baseResponse.Success(result);
                else
                    return await _baseResponse.CustomErrorMessage("Failed to save record.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }
        public virtual async Task<IBaseResult<object>> Delete(int id)
        {
            try
            {
                var result = _baseRepository.GetById(id);
                if (result != null)
                {
                    var dstatus = Convert.ToBoolean(result.GetType().GetProperty("IsDeleted").GetValue(result));
                    if (dstatus)
                        return await _baseResponse.CustomErrorMessage("This Record has been deleted already.", HttpStatusCode.BadRequest);



                    //delete record
                    _baseRepository.Remove(result);

                    return await _baseResponse.CustomResponse(HttpStatusCode.OK, null, "00", "Record successfully deleted.");
                }
                else
                    return await _baseResponse.CustomErrorMessage("Record not found.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }



        public virtual async Task<IBaseResult<object>> AddMultiple(IEnumerable<T> dto)
        {
            try
            {
                // map model to new account object
                var mappedObject = _mapper.Map<IList<TEntity>>(dto);

                _baseRepository.AddRange(mappedObject);
                return await _baseResponse.Success(dto);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }

        public virtual async Task<IBaseResult<object>> UpdateMultiple(IEnumerable<T> dto)
        {
            try
            {
                var ids = dto.Select(x => x.GetType().GetProperty("Id").GetValue(x));
                var items = _baseRepository.Find(x => ids.Contains((EF.Property<int>(x, "Id"))));
                // map model to new account object
                var mappedObject = _mapper.Map<IEnumerable<T>, IEnumerable<TEntity>>(dto, items);

                var result = _baseRepository.UpdateRange(mappedObject.ToList());
                if (result != null)
                    return await _baseResponse.Success(result);
                else
                    return await _baseResponse.CustomErrorMessage("Could not modify records.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex);
            }
        }
    }
}
