//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeAndCake.BDSHKA
{
    using System;
    using System.Collections.Generic;
    
    public partial class CookingStage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CookingStage()
        {
            this.IngredientOfStage = new HashSet<IngredientOfStage>();
        }
    
        public int Id { get; set; }
        public int DishId { get; set; }
        public string ProcessDescription { get; set; }
        public Nullable<int> TimeInMinutes { get; set; }
    
        public virtual Dish Dish { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredientOfStage> IngredientOfStage { get; set; }
    }
}
