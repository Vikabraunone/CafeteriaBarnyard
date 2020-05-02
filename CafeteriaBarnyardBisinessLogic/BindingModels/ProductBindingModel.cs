namespace CafeteriaBarnyardBisinessLogic.BindingModels
{
    public class ProductBindingModel
    {
        public int? Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public double? FillWeight { get; set; }
    }
}
