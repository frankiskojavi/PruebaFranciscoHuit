<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/PHFMaestro.Master" AutoEventWireup="true" CodeBehind="TestDelete.aspx.cs" Inherits="PruebaHabilidadesFranciscoHuit.FrontEnd.TestDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button"  OnClick="Button1_Click"/>
    <br><br><br>
    <asp:Image ID="imagenPrueba" runat="server" Height="193px" Width="256px" />
</asp:Content>
