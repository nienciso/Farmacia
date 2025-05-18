<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="ABMArticulos.aspx.cs" Inherits="Presentacion.ABMArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%; height:100vh; text-align:center; margin: 0; padding: 0; border:none; color: #4CAF50;"">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 400px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px;">ABM de Artículos</h2>

                    <div style="margin-bottom: 15px;"> 
                        <label for="txtCodigo" style="display: block;">Código:</label>
                        <asp:TextBox ID="txtCodigo" runat="server" Width="100%" MaxLength="10" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px; text-transform: uppercase;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtNombre" style="display: block;">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="ddlTipoPresentacion" style="display: block;">Tipo de Presentación:</label>
                        <asp:DropDownList ID="ddlTipoPresentacion" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;">
                            <asp:ListItem Text="Unidad" Value="Unidad"></asp:ListItem>
                            <asp:ListItem Text="Blíster" Value="Blíster"></asp:ListItem>
                            <asp:ListItem Text="Sobre" Value="Sobre"></asp:ListItem>
                            <asp:ListItem Text="Frasco" Value="Frasco"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtTamaño" style="display: block;">Tamaño:</label>
                        <asp:TextBox ID="txtTamaño" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtPrecio" style="display: block;">Precio:</label>
                        <asp:TextBox ID="txtPrecio" runat="server" Width="100%" TextMode="Number" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtCodigoCategoria" style="display: block;">Código de Categoría:</label>
                        <asp:TextBox ID="txtCodigoCategoria" runat="server" Width="100%" MaxLength="6" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 20px;">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    </div>

                    <div>
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="100%" OnClick="btnAgregar_Click" style="padding: 10px; background-color: #4CAF50; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;"/>
                    </div>
                    
                    <div style="margin-top: 10px;">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Width="100%" OnClick="btnModificar_Click" style="padding: 10px; background-color: #FFC107; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;"/>
                    </div>

                    <div style="margin-top: 10px;">
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Width="100%" OnClick="btnEliminar_Click" style="padding: 10px; background-color: #F44336; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;"/>
                    </div>

                </div>
            </td>
        </tr>
    </table>
</asp:Content>

