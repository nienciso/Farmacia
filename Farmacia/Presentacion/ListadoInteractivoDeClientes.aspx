<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="ListadoInteractivoDeClientes.aspx.cs" Inherits="Presentacion.ListadoInteractivoDeClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%; height: 100vh; text-align: center; margin: 0; padding: 0; border: none;">
        <tr>
            <td style="vertical-align: top; padding-top: 30px;">
                <div style="width: 900px; margin: auto; padding: 25px; border: 1px solid #ddd; border-radius: 12px; background-color: #fff; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1); display: inline-block; text-align: left;">
                    
                    <h2 style="text-align: center; margin-bottom: 30px; color: #4CAF50;">Resumen de Cliente</h2>

                    <h4 style="margin-top: 0; color: #4CAF50;">Listado de Clientes</h4>
                    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="gridview"
                        OnSelectedIndexChanged="gvClientes_SelectedIndexChanged" BorderWidth="1px" BorderColor="#ddd" 
                        CellPadding="8" GridLines="None" Style="border-radius: 10px; overflow: hidden;">
                        <HeaderStyle BackColor="#4CAF50" ForeColor="White" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#f9f9f9" />
                        <Columns>
                            <asp:BoundField DataField="Cedula" HeaderText="Cédula" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Ver Detalle" />
                        </Columns>
                    </asp:GridView>

                    <h4 style="margin-top: 30px; color: #4CAF50;">Detalles del Cliente</h4>
                    <p><strong>Cédula:</strong> <asp:Label ID="lblCedulaCliente" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Nombre:</strong> <asp:Label ID="lblNombreCliente" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Número tarjeta:</strong> <asp:Label ID="lblNumeroTarjetaCliente" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Teléfono:</strong> <asp:Label ID="lblTelefonoCliente" runat="server" Text="-"></asp:Label></p>

                    <h4 style="margin-top: 30px; color: #4CAF50;">Ventas Asociadas</h4>
                    <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False" Width="100%" 
                        OnSelectedIndexChanged="gvVentas_SelectedIndexChanged" BorderWidth="1px" BorderColor="#ddd"
                        CellPadding="8" GridLines="None" Style="border-radius: 10px; overflow: hidden;">
                        <HeaderStyle BackColor="#4CAF50" ForeColor="White" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#f2f2f2" />
                        <Columns>
                            <asp:BoundField DataField="NumeroVenta" HeaderText="Número de Venta" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Ver Detalle" />
                        </Columns>
                    </asp:GridView>

                    <h4 style="margin-top: 30px; color: #4CAF50;">Detalles de la Venta</h4>
                    <p><strong>Venta Seleccionada:</strong> <asp:Label ID="lblVentaSeleccionada" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Fecha de Venta:</strong> <asp:Label ID="lblFechaVenta" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Estado de la Venta:</strong> <asp:Label ID="lblEstadoVenta" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Dirección de Entrega:</strong> <asp:Label ID="lblDireccionVenta" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Empleado Responsable:</strong> <asp:Label ID="lblEmpleadoVenta" runat="server" Text="-"></asp:Label></p>
                    <p><strong>Cliente Asociado:</strong> <asp:Label ID="lblClienteVenta" runat="server" Text="-"></asp:Label></p>

                    <h4 style="margin-top: 30px; color: #4CAF50;">Artículos Comprados</h4>
                    <asp:GridView ID="gvArticulosComprados" runat="server" AutoGenerateColumns="False" Width="100%"
                        BorderWidth="1px" BorderColor="#ddd" CellPadding="8" GridLines="None"
                        Style="border-radius: 10px; overflow: hidden;">
                        <HeaderStyle BackColor="#4CAF50" ForeColor="White" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#f9f9f9" />
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Artículo" SortExpression="Nombre" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" SortExpression="Precio" />
                        </Columns>
                    </asp:GridView>

                    <h4 style="margin-top: 30px; color: #4CAF50;">Monto Total Gastado</h4>
                    <p><strong>Total:</strong> <asp:Label ID="lblMontoTotal" runat="server" Text="-"></asp:Label></p>

                </div>
            </td>
        </tr>
    </table>
</asp:Content>