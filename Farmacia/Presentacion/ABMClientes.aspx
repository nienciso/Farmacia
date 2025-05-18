<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="ABMClientes.aspx.cs" Inherits="Presentacion.ABMClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%; height:100vh; text-align:center; margin: 0; padding: 0; border:none; color: #4CAF50;"">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 400px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px;">ABM de Clientes</h2>

                    <div style="margin-bottom: 15px;"> 
                        <label for="txtCI" style="display: block;">Cédula de Identidad:</label>
                        <asp:TextBox ID="txtCI" runat="server" Width="100%" MaxLength="8" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtNombre" style="display: block;">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtTarjeta" style="display: block;">Número de Tarjeta:</label>
                        <asp:TextBox ID="txtTarjeta" runat="server" Width="100%" MaxLength="16" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="txtTelefono" style="display: block;">Teléfono:</label>
                        <asp:TextBox ID="txtTelefono" runat="server" Width="100%" MaxLength="9" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
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
