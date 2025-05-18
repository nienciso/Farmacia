<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="ListadoInteractivoDeArticulos.aspx.cs" Inherits="Presentacion.ListadoInteractivoDeArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%; height: 100vh; text-align: center; margin: 0; padding: 0; border: none;">
        <tr>
            <td style="vertical-align: top; padding-top: 30px;">
                <div style="width: 900px; margin: auto; padding: 25px; border: 1px solid #ddd; border-radius: 12px; background-color: #fff; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1); display: inline-block; text-align: left;">
                    
                    <h2 style="text-align: center; margin-bottom: 30px; color: #4CAF50;">Listado Interactivo de Artículos</h2>

                    <div style="margin-bottom: 15px;">
                        <label for="ddlCategoria" style="display: block; font-weight: bold;">Seleccionar Categoría:</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;">
                            <asp:ListItem Text="Seleccione una categoría..." Value="" />
                        </asp:DropDownList>
                    </div>

                    <div style="margin-bottom: 15px;">
                        <label for="ddlArticulo" style="display: block; font-weight: bold;">Seleccionar Artículo:</label>
                        <asp:DropDownList ID="ddlArticulo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArticulo_SelectedIndexChanged" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;">
                            <asp:ListItem Text="Seleccione un artículo..." Value="" />
                        </asp:DropDownList>
                    </div>

                    <div style="margin-bottom: 15px; text-align: left;">
                        <h4 style="color: #4CAF50;">Detalles del Artículo</h4>
                        <p><strong>Código:</strong> <asp:Label ID="lblCodigo" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Nombre:</strong> <asp:Label ID="lblNombre" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Categoría:</strong> <asp:Label ID="lblCategoria" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Presentación:</strong> <asp:Label ID="lblPresentacion" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Tamaño:</strong> <asp:Label ID="lblTamaño" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Precio:</strong> <asp:Label ID="lblPrecio" runat="server" Text="-"></asp:Label></p>
                    </div>

                    <div style="margin-bottom: 15px;">
                        <h4 style="color: #4CAF50;">Ventas del Artículo</h4>
                        <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvVentas_SelectedIndexChanged"
                            Width="100%" CssClass="gridview" style="border: 1px solid #ccc; border-radius: 4px; padding: 10px;">
                            <Columns>
                                <asp:BoundField DataField="NumeroVenta" HeaderText="Número Venta" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:CommandField ShowSelectButton="True" SelectText="Ver Detalle" />
                            </Columns>
                        </asp:GridView>

                        <asp:Literal ID="lblMensaje" runat="server" EnableViewState="false" />
                    </div>

                    <div style="margin-bottom: 15px; text-align: left;">
                        <h4 style="color: #4CAF50;">Detalles de la Venta</h4>
                        <p><strong>Número de Venta:</strong> <asp:Label ID="lblNumVenta" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Fecha:</strong> <asp:Label ID="lblFechaVenta" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Estado:</strong> <asp:Label ID="lblEstadoVenta" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Dirección Envío:</strong> <asp:Label ID="lblDireccion" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Cantidad Vendida:</strong> <asp:Label ID="lblCantidad" runat="server" Text="-"></asp:Label></p>
                        <p><strong>Cliente:</strong> <asp:Label ID="lblCliente" runat="server" Text="-"></asp:Label></p>
                    </div>

                </div>
            </td>
        </tr>
    </table>
</asp:Content>
