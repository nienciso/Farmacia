<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="Bienvenida.aspx.cs" Inherits="Presentacion.Bienvenida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Menú Principal</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 494px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="border-style: none; border-color: inherit; border-width: medium; text-align:center; margin: 0; padding: 0; " class="auto-style1">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 350px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px; color: #4CAF50;"">Menú Principal</h2>

                    <ul style="list-style: none; padding: 0;">
                        <li style="margin-bottom: 10px;">
                            <asp:HyperLink ID="hlCategorias" runat="server" NavigateUrl="ABMCategorias.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                ABM de Categorías
                            </asp:HyperLink>
                        </li>
                          <br />
                        <li>
                            <asp:HyperLink ID="hlArticulos" runat="server" NavigateUrl="ABMArticulos.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                ABM de Artículos
                            </asp:HyperLink>
                        </li>
                          <br />
                        <li>
                            <asp:HyperLink ID="hlClientes" runat="server" NavigateUrl="ABMClientes.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                ABM de Clientes
                            </asp:HyperLink>
                        </li>
                          <br />
                        <li>
                            <asp:HyperLink ID="hlVenta" runat="server" NavigateUrl="AltaDeVenta.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                Alta de Venta
                            </asp:HyperLink>
                        </li>
                          <br />
                        <li>
                            <asp:HyperLink ID="hlSeguimiento" runat="server" NavigateUrl="~/SeguimientoDeVenta.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                Seguimiento de Venta
                            </asp:HyperLink>
                        </li>
                          <br />
                        <li>
                            <asp:HyperLink ID="hlListado" runat="server" NavigateUrl="~/ListadoInteractivoDeArticulos.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                Listado Interactivo de Artículos
                            </asp:HyperLink>
                        </li>
                       <br />
                        <li style="margin-bottom: 10px;">
                            <asp:HyperLink ID="hlListadoCliente" runat="server" NavigateUrl="~/ListadoInteractivoDeClientes.aspx"
                                style="display: block; padding: 10px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;">
                                Listado Interactivo De Clientes
                            </asp:HyperLink>
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
