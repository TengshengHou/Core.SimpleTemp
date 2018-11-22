using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Authorization
{
    public static class UserPermission
    {
        public const string UserController_Index = "UserController_Index";
        public const string UserController_Edit = "UserController_Edit";
        public const string UserController_DeleteMuti = "UserController_DeleteMuti";
        public const string UserController_Delete = "UserController_Delete";
        public const string UserController_Get = "UserController_Get";

        public const string UserController_GetUserByDepartment = "UserController_GetUserByDepartment";
    }

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
}
