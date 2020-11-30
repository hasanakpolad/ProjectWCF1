using ProjectWCF1.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWCF1.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjectEntities _context = new ProjectEntities();
        private Repository<UserDto> _userRepository;
        private Repository<ProjectDto> _projectRepository;
        private Repository<ProjectRoleDto> _roleRepository;

        public Repository<UserDto> userRepository => _userRepository ?? (_userRepository = new Repository<UserDto>(_context));
        public Repository<ProjectDto> projectRepository => _projectRepository ?? (_projectRepository = new Repository<ProjectDto>(_context));
        public Repository<ProjectRoleDto> roleRepository => _roleRepository ?? (_roleRepository = new Repository<ProjectRoleDto>(_context));

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}