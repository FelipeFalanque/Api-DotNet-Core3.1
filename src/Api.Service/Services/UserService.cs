using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces.Helpers.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IUserHelper _userHelper;
        private readonly IMapper _mapper;

        public UserService(
            IRepository<UserEntity> repository,
            IUserHelper userHelper,
            IMapper mapper)
        {
            _repository = repository;
            _userHelper = userHelper;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return entity != null ? _mapper.Map<UserDTO>(entity) : null;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var listEntity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(listEntity);
        }

        public async Task<UserCreateResultDTO> Post(UserCreateDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            _userHelper.AdicionarPrefixoNome(ref entity);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<UserCreateResultDTO>(result);
        }
        
        public async Task<UserUpdateResultDTO> Put(UserUpdateDTO user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            _userHelper.AdicionarPrefixoNome(ref entity);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserUpdateResultDTO>(result);
        }

    }
}
