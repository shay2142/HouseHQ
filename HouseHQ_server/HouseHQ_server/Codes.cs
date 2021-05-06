using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHQ_server
{
    public class Codes
    {
        public Dictionary<string, string> codes()
        {
            return new Dictionary<string, string>()
            {
                {"101", "login"},
                {"102", "singup"},
                {"103", "change account"},
                {"104", "add apps"},
                {"105", "all apps"},
                {"106", "delete apps"},
                {"107", "delete apps from user"},
                {"108", "add apps for user"},
                {"109", "logout"},
                {"110", "delete user"},
                {"111", "getAllUsers"},
                {"112", "getAllUsers"},
                {"113", "sent DB"},
                {"114", "get user apps"},
                {"115", "sent logs"},
                {"116", "add level"},
                {"117", "get level key"},
                {"118", "delete app for level"},
                {"119", "delete level"},
                {"120", "update apps for level"},
                {"121", "delete logs"},
                {"122", "getAllAppsOnPC" },
                {"400", "error"},
                {"201", "ok login"},
                {"202", "ok singup"},
                {"203", "ok change account"},
                {"204", "ok add apps"},
                {"205", "ok all apps"},
                {"206", "ok delete apps"},
                {"207", "ok delete apps from user"},
                {"208", "ok add apps for user"},
                {"209", "ok logout"},
                {"210", "ok delete user"},
                {"211", "ok getAllUsers"},
                {"212", "ok getAllUsers"},
                {"213", "ok sent DB"},
                {"214", "ok get user apps"},
                {"215", "ok sent logs"},
                {"216", "ok add level"},
                {"217", "ok get level key"},
                {"218", "ok delete app for level"},
                {"219", "ok delete level"},
                {"220", "ok update apps for level"},
                {"221", "ok delete logs"},
                {"222", "ok getAllAppsOnPC" }
            };
        }
    }
}
