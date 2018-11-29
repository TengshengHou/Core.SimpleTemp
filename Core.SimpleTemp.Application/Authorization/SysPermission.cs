using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Application.Authorization
{
    /// <summary>
    /// 用户授权FucntionPermissionCode
    /// </summary>
    public static class UserPermission
    {
        public const string UserController_Index = "UserController_Index";
        public const string UserController_Edit = "UserController_Edit";
        public const string UserController_DeleteMuti = "UserController_DeleteMuti";
        public const string UserController_Delete = "UserController_Delete";
        public const string UserController_Get = "UserController_Get";

        public const string UserController_GetUserByDepartment = "UserController_GetUserByDepartment";
    }
    /// <summary>
    /// 组织结构授权FucntionPermissionCode
    /// </summary>
    public static class DepartmentPermission
    {
        public const string Department_Index = "Department_Index";
        public const string Department_Edit = "Department_Edit";
        public const string Department_DeleteMuti = "Department_DeleteMuti";
        public const string Department_Delete = "Department_Delete";
        public const string Department_Get = "Department_Get";

        public const string Department_GetTreeData = "Department_GetTreeData";
        public const string Department_GetChildrenByParent = "Department_GetChildrenByParent";
    }
    /// <summary>
    /// 功能菜单授权FucntionPermissionCode
    /// </summary>
    public static class MenuPermission
    {
        public const string Menu_Index = "Menu_Index";
        public const string Menu_Edit = "Menu_Edit";
        public const string Menu_DeleteMuti = "Menu_DeleteMuti";
        public const string Menu_Delete = "Menu_Delete";
        public const string Menu_Get = "Menu_Get";

        public const string Menu_GetMenuTreeData = "Menu_GetMenuTreeData";
        public const string Menu_GetMneusByParent = "Menu_GetMneusByParent";
    }
    /// <summary>
    /// 角色授权FucntionPermissionCode
    /// </summary>
    public static class RolePermission
    {
        public const string Role_Index = "Role_Index";
        public const string Role_Edit = "Role_Edit";
        public const string Role_DeleteMuti = "Role_DeleteMuti";
        public const string Role_Delete = "Role_Delete";
        public const string Role_Get = "Role_Get";

        public const string Role_GetAllPageList = "Role_GetAllPageList";
        public const string Role_GetMenusByRole = "Role_GetMenusByRole";
        public const string Role_SavePermission = "Role_SavePermission";
    }
}
