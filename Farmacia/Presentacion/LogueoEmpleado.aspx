<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPPublica.Master" AutoEventWireup="true" CodeBehind="LogueoEmpleado.aspx.cs" Inherits="Presentacion.LogueoEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%; height:100vh; text-align:center; margin: 0; padding: 0; border:none;">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 300px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px;">Iniciar sesión</h2>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="txtUsuario" style="display: block;">Usuario:</label>
                        <asp:TextBox ID="txtUsuario" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"/>
                    </div>
                    
                    <div style="margin-bottom: 20px;">
                        <label for="txtContrasena" style="display: block;">Contraseña:</label>
                        <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"/>
                    </div>
                    
                    <div>
                        <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesión" Width="100%" OnClick="btnIniciarSesion_Click" style="padding: 10px; background-color: #4CAF50; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;"/>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
