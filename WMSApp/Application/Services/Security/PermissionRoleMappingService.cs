using WMS.Core;
using WMS.Core.Data;
using Domain.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Security
{
    public partial class PermissionRoleMappingService : IPermissionRoleMappingService
    {

        #region Fields
        private readonly IRepository<PermissionMaster_Role_Mapping> _permissionMaster_Role_MappingRepository;

        private readonly IRepository<PermissionMaster> _permissionMasterRepository;

        #endregion

        #region Ctor
        public PermissionRoleMappingService(IRepository<PermissionMaster_Role_Mapping> permissionMaster_Role_MappingRepository,
              IRepository<PermissionMaster> permissionMasterRepository)
        {
            _permissionMaster_Role_MappingRepository = permissionMaster_Role_MappingRepository;

            _permissionMasterRepository = permissionMasterRepository;

        }
        #endregion

        #region Methods
        public virtual void Insert(List<PermissionMaster_Role_Mapping> entities)
        {
            _permissionMaster_Role_MappingRepository.Insert(entities);
        }
        public virtual void Update(List<PermissionMaster_Role_Mapping> entities)
        {
            foreach (var item in entities)
            {
                var query = _permissionMaster_Role_MappingRepository.Table;
                var mapping = query.FirstOrDefault(x => x.Permission_Id == item.Permission_Id && x.Role_ID == item.Role_ID);
                if (mapping != null)
                {
                    var m = _permissionMaster_Role_MappingRepository.GetById(mapping.Id);
                    m.Add = item.Add;
                    m.Edit = item.Edit;
                    m.View = item.View;
                    m.Delete = item.Delete;
                    _permissionMaster_Role_MappingRepository.Update(m);
                }
                else
                {
                    _permissionMaster_Role_MappingRepository.Insert(item);
                }

            }
            //string roleid = entities.FirstOrDefault().Role_ID;
            //var mongoquery = from x in _permissionMaster_Role_MappingRepository.Table
            //                 join y in _permissionMasterRepository.Table
            //                     on x.Permission_Id equals y.ID
            //                 where x.Role_ID == roleid
            //                 select new Permission
            //                 {

            //                     Add = x.Add,
            //                     Delete = x.Delete,
            //                     Edit = x.Edit,
            //                     ID = x.ID,
            //                     Permission_Id = x.Permission_Id,
            //                     Role_ID = x.Role_ID,
            //                     View = x.View,
            //                     PermissionMaster = y.SystemName


            //                 };

            //var filter = Builders<Permission>.Filter.Eq(x => x.Role_ID, roleid);
            //_mongoRepository.RemoveMany(filter);
            //_mongoRepository.InsertMany(mongoquery.ToList());

        }

        public List<PermissionMaster_Role_Mapping> GetPermissionMapListRoleWise(string roleId)
        {
            //var query = _permissionMaster_Role_MappingRepository.Table;
            //query = query.Where(x => x.Role_ID == roleId)
            //    .Include(s => s.PermissionMaster);
            //return query.ToList();



            //var query = from pm in _permissionMasterRepository.Table
            //            join rm in _permissionMaster_Role_MappingRepository.Table
            //            on new { pm.Id, roleId } equals new { ID = rm.Permission_Id, roleId = rm.Role_ID }
            //            select rm;


            var query = from pm in _permissionMasterRepository.Table
                        join rm in _permissionMaster_Role_MappingRepository.Table
                        on new { pm.Id, roleId } equals new { Id = rm.Permission_Id, roleId = rm.Role_ID }
                        select rm;


            var result = new PagedList<PermissionMaster_Role_Mapping>(query, 0, int.MaxValue);
            return result.ToList();



        }
        #endregion
    }
}
