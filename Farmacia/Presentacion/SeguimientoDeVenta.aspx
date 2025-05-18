<%@ Page Title="" Language="C#" MasterPageFile="~/UnaMPEmpleado.Master" AutoEventWireup="true" CodeBehind="SeguimientoDeVenta.aspx.cs" Inherits="Presentacion.SeguimientoDeVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%; height:100vh; text-align:center; margin: 0; padding: 0; border:none; color: #4CAF50;">
        <tr>
            <td style="vertical-align: middle; text-align:center;">
                <div style="width: 300px; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #fff; box-shadow: 0 4px 8px rgba(0,0,0,0.1); display:inline-block;">
                    <h2 style="margin-bottom: 20px;">Seguimiento de Venta</h2>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="txtNumeroVenta" style="display: block;">Número de Venta:</label>
                        <asp:TextBox ID="txtNumeroVenta" runat="server" Width="100%" style="padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:TextBox>
                    </div>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="lblEstadoActual" style="display: block;">Estado Actual:</label>
                        <asp:Label ID="lblEstadoActual" runat="server" Text="-" ForeColor="Black" style="display: block; padding: 8px; border: 1px solid #ccc; border-radius: 4px; background-color: #f9f9f9;"></asp:Label>
                    </div>
                    
                    <div style="margin-bottom: 15px;">
                        <label for="lblEstadoSiguiente" style="display: block;">Próximo Estado:</label>
                        <asp:Label ID="lblEstadoSiguiente" runat="server" Text="-" ForeColor="Blue" style="display: block; padding: 8px; border: 1px solid #ccc; border-radius: 4px; background-color: #e8f4ff;"></asp:Label>
                    </div>
                    
                    <div style="margin-bottom: 20px;">
                        <asp:Button ID="btnVerificar" runat="server" Text="Verificar Estado" Width="100%" OnClick="btnVerificar_Click" style="padding: 10px; background-color: #FFC107; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;" />
                    </div>
                    
                    <div style="margin-bottom: 20px;">
                        <asp:Button ID="btnConfirmarCambio" runat="server" Text="Confirmar Cambio" Width="100%" OnClick="btnConfirmarCambio_Click" Enabled="false" style="padding: 10px; background-color: #28a745; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;" />
                    </div>
                    
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" style="display: block; margin-top: 10px;"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
