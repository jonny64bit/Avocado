using System.ComponentModel.DataAnnotations;
using Pact.Web.Vue.Grid.Interfaces;

namespace Avocado.Web.Models.Accounts
{
    public class AccountEditItem : IEdit
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}