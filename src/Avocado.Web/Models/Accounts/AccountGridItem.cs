using Pact.Web.Vue.Grid.Interfaces;

namespace Avocado.Web.Models.Accounts
{
    public class AccountGridItem : IGridRow
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}