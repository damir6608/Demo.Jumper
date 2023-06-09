//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo.Jumper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Windows.Media.Imaging;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.ProductSales = new HashSet<ProductSale>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Icon { get; set; }
        public string Address { get; set; }
        public string Priority { get; set; }
        public string Director { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSales { get; set; }

        [NotMapped]
        public BitmapImage AgentPhotoFromResources => !string.IsNullOrEmpty(Icon)
            ? new BitmapImage(new Uri("C:/Users/latyp/Desktop/4 ���� 4432/���.���/Demo.Jumper/Photos" + Icon.Replace("\\", "/")))
                : new BitmapImage(new Uri("C:/Users/latyp/Desktop/4 ���� 4432/���.���/Demo.Jumper/Photos/picture.png"));

    }
}
