using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        private Boolean good = false;

        BindingSource showProductList = new BindingSource();

        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
                {
                    "Beverages",
                    "Bread/Bakery",
                    "Canned/Jarred Goods",
                    "Dairy",
                    "Frozen Goods",
                    "Meat",
                    "Personal Care",
                    "Other"
                };

            foreach (string item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException();
            }
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
            {
                throw new NumberFormatException();
            }
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException();
            }
            return Convert.ToDouble(price);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                good = false;

                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy=MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;

                good = true;
            } //end of try

            catch (NumberFormatException nfe)
            {
                MessageBox.Show("Please check the quantity. It must contain only numbers.");
            } //end of catch1
            catch (StringFormatException sfe)
            {
                MessageBox.Show("Please check the product name. It must contain only letters.");
            } //end of catch2
            catch (CurrencyFormatException cfe)
            {
                MessageBox.Show("Please check the price. It must contain only numbers.");
            } //end of catch3
            finally
            {
                if (good == true)
                {
                    MessageBox.Show("Product has been added!");
                }
            } //end of finally

        }
    }

    // Custom exceptions
    public class NumberFormatException : Exception
    {
        public NumberFormatException(string varName) : base(varName) { }
        public NumberFormatException() { }
    };
    public class StringFormatException : Exception
    {
        public StringFormatException(string varName) : base(varName) { }
        public StringFormatException() { }
    };
    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string varName) : base(varName) { }
        public CurrencyFormatException() { }
    }

}
