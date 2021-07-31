using AutoMapper;
using Avocado.Database;
using Avocado.Database.Models;
using Avocado.Web.Models.Accounts;
using Pact.Web.Vue.Grid.Controllers;

namespace Avocado.Web.Controllers
{
    public class AccountsController : DefaultCRUDController<Account, AccountGridItem, AccountEditItem>
    {
        public AccountsController(DAL context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}