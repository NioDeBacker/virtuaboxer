
using Microsoft.AspNetCore.Components;
using System;

namespace Client.Layout;

public class Header : IDisposable
{
    private bool isOpen;
    private string isOpenClass => isOpen ? "is-active" : null;

    //[Inject] public ISidepanelService Sidepanel { get; set; }
    //[Inject] public Cart Cart { get; set; }



    public void Dispose()
    {
        //Cart.OnCartChanged -= StateHasChanged;
    }

    private void ToggleMenuDisplay()
    {
        isOpen = !isOpen;
    }

    private void OpenShoppingCart()
    {
        //Sidepanel.Open<ShoppingCart>("Winkelwagen");
    }


}