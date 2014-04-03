<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="KenoResult.aspx.cs" Inherits="Fanex.Result.WebFormTest.KenoResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Keno Result page
    </h3>
    <asp:Literal ID="KenoReulst" runat="server"></asp:Literal>
</asp:Content>
