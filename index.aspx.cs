using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wed_ASP.NET
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            panOrder.Visible = panPricing.Visible = false;

            if (!Page.IsPostBack)
            {
                lblAddresse.Visible = txtAddresse.Visible = false;
                //
                cboPizza.Items.Add(new ListItem("Vegetarian", "10"));
                cboPizza.Items.Add(new ListItem("Hawaiians", "12"));
                cboPizza.Items.Add(new ListItem("All Dressed", "8"));
                cboPizza.Items.Add(new ListItem("Halal", "20"));
                //
                lstPizzaSize.Items.Add(new ListItem("Small", "1"));
                lstPizzaSize.Items.Add(new ListItem("Medium", "1.5"));
                lstPizzaSize.Items.Add(new ListItem("Large", "2"));
                //
                lstPizzaSize.SelectedIndex = 0;
                //
                chkToppings.Items.Add(new ListItem("Bacon", "3"));
                chkToppings.Items.Add(new ListItem("Mushroom", "1.5"));
                chkToppings.Items.Add(new ListItem("Extra Cheese", "2"));
                chkToppings.Items.Add(new ListItem("Olive", "2.5"));
                //
                radlstCrust.Items.Add(new ListItem("Normal"));
                radlstCrust.Items.Add(new ListItem("Thin"));
                radlstCrust.Items.Add(new ListItem("Thick"));
                //
                radlstCrust.SelectedIndex = 0;

            } 

            if (cboPizza.SelectedIndex > 0)
            {
                calculatePrice();
            }
            
        }

        private void calculatePrice()
        {
            decimal baseP = 0, delivery = 0, total = 0, toppings = 0, tax = 0, subTotal = 0;
            //
            baseP = Convert.ToDecimal(cboPizza.SelectedItem.Value) * Convert.ToDecimal(lstPizzaSize.SelectedItem.Value);
            //
            litPricing.Text = "<br/><b>Base Price: $</b>" + baseP;
            if (chkDeliver.Checked)
            {
                delivery = 3;
                litPricing.Text += "<br/><b>Delivery Price: $</b>" + delivery;
            }

            foreach (ListItem item in chkToppings.Items)
            {
                // If an item is selected, add the value to the toppings, else add 0
                // Ternary Operation
                // += recursion
                toppings += (item.Selected) ? Convert.ToDecimal(item.Value) : 0;
            }

            subTotal = baseP + delivery + toppings;
            tax = Math.Round(subTotal * Convert.ToDecimal(0.15), 2);
            total = Math.Round(tax + subTotal, 2);
            litPricing.Text += "<br/><b>Toppings Price: $</b>" + toppings;
            litPricing.Text += "<br/><b>SubTotal Price: $</b>" + subTotal;
            litPricing.Text += "<br/><b>Tax Price: $</b>" + tax;
            litPricing.Text += "<br/><b>Total Price: $</b>" + total + "<br/>";

            panPricing.Visible = true;
        }

        protected void chkDeliver_CheckedChanged(object sender, EventArgs e)
        {
            lblAddresse.Visible = txtAddresse.Visible = chkDeliver.Checked;

            // lblAddresse.Visible = txtAddresse.Visible = (chkDeliver.Checked) ? true : false;
        }

        protected void cboPizza_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnOrderNow_Click(object sender, EventArgs e)
        {
            panOrder.Visible = true;

            litOrder.Text = "Sir/Miss " + txtCustomer.Text + ",<br/>Your order for a " + lstPizzaSize.SelectedItem.Text
                + "<br/>" + cboPizza.SelectedItem.Text + " Pizza<br/>with toppings of:<br/><ul>";

            foreach (ListItem item in chkToppings.Items)
            {
                litOrder.Text += item.Selected ? "<li>" + item.Text + "</li>" : "";
            }

            litOrder.Text += "</ul>On a " + radlstCrust.SelectedItem.Text + " crust<br/>";

            if (chkDeliver.Checked)
            {
                litOrder.Text += "Will be delivey to: " + txtAddresse.Text;
            } else
            {
                litOrder.Text += "Will be ready to pick-up.";
            }

        }
    }
}