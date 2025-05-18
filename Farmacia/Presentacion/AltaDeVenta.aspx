<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="AltaDeVenta.aspx.cs" Inherits="Presentacion.AltaDeVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%; height:100vh; text-align:center; margin: 0; padding: 0; border:none; color: #4CAF50;"">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 300px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px;">Alta de Venta</h2>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="ddlArticulo" style="display: block;">Artículo:</label>
                        <asp:DropDownList ID="ddlArticulo" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:DropDownList>
                    </div>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="txtCantidad" style="display: block;">Cantidad:</label>
                        <asp:TextBox ID="txtCantidad" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:TextBox>
                    </div>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="ddlCliente" style="display: block;">Cliente:</label>
                        <asp:DropDownList ID="ddlCliente" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:DropDownList>
                    </div>
                    
                    <div style="margin-bottom: 20px;">
                        <label for="txtDireccion" style="display: block;">Dirección de envío:</label>
                        <asp:TextBox ID="txtDireccion" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:TextBox>
                    </div>
                    
                    <div>
                        <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" Width="100%" OnClick="btnRegistrarVenta_Click" style="padding: 10px; background-color: #4CAF50; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;" />
                    </div>
                    
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" style="display: block; margin-top: 10px;"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
