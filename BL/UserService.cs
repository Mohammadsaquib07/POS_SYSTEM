//using System.Collections.Generic;
//using System.Threading.Tasks;
//using MyApp.Models;
//using Products_Crud.Interfaces;

//namespace Products_Crud.BL
//{
//    public class UserService : IUserService
//    {
//        private readonly IUserRepository _repo;

//        public UserService(IUserRepository repo)
//        {
//            _repo = repo;
//        }

//        public Task<User?> GetByIdAsync(int id)
//        {
//            return _repo.GetByIdAsync(id);
//        }

//        public Task<IEnumerable<User>> GetAllAsync()
//        {
//            return _repo.GetAllAsync();
//        }

//        public Task<User> CreateAsync(User user)
//        {
//            return _repo.CreateAsync(user);
//        }

//        public Task UpdateAsync(User user)
//        {
//            return _repo.UpdateAsync(user);
//        }

//        public Task DeleteAsync(int id)
//        {
//           var existing = _repo.GetByIdAsync(id);
//            if (existing is null)
//                return null;
//            var result = new User();
//            result = existing.Result;
//            return _repo.DeleteAsync(result);
//        }

//        public Task DeleteAsync(User user)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}