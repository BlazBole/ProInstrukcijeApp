using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stantehnika.Model
{
    public class UPNplacilniNalog
    {
        public string UPNQR { get; set; } // Leading style
        public string IBANPlačnika { get; set; } // IBAN of the payer
        public string Polog { get; set; } // Deposit
        public string Dvig { get; set; } // Withdrawal
        public string ReferencaPlačnika { get; set; } // Reference of the payer
        public string ImePlačnika { get; set; } // Name of the payer
        public string UlicaInŠtevilkaPlačnika { get; set; } // Street and number of the payer
        public string KrajPlačnika { get; set; } // City of the payer
        public string Znesek { get; set; } // Amount
        public string DatumPlačila { get; set; } // Payment date
        public string Nujno { get; set; } // Urgent
        public string KodaNamena { get; set; } // Purpose code
        public string NamenPlačila { get; set; } // Purpose of payment
        public string RokPlačila { get; set; } // Due date
        public string IBANPrejemnika { get; set; } // IBAN of the recipient
        public string ReferencaPrejemnika { get; set; } // Reference of the recipient
        public string ImePrejemnika { get; set; } // Name of the recipient
        public string UlicaInŠtevilkaPrejemnika { get; set; } // Street and number of the recipient
        public string KrajPrejemnika { get; set; } // City of the recipient
        public int VsotaZnakov { get; set; } // Total number of characters
    }
}
