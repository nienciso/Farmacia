<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPPublica.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%; height:100vh; text-align:center; margin: 0; padding: 0; border:none;">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 600px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px;">Ventas Pendientes</h2>

                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" />
                    
                    <asp:GridView ID="gvVentasPendientes" runat="server" AutoGenerateColumns="False" Width="100%" BorderWidth="1px" BorderColor="#ddd" CellPadding="8" GridLines="None" Style="border-radius: 8px; overflow: hidden;">
                        <HeaderStyle BackColor="#4CAF50" ForeColor="White" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#f2f2f2" />
                        <Columns>
                            <asp:BoundField DataField="NumeroVenta" HeaderText="N° Venta" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha Realizada" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección de Envío" />
                        </Columns>
                    </asp:GridView>

            </td>
        </tr>
    </table>
</asp:Content>
