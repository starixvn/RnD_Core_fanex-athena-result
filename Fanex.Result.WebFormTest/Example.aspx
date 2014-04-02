<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Example.aspx.cs" Inherits="Fanex.Result.WebFormTest.Example" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="example-item">
        <li><a class="button-normal" href="Normal.aspx">Normal </a></li>
        <li><a class="button-normal" href="OutRight.aspx">OutRight </a></li>
        <li><a class="button-normal" href="Parlay.aspx">PopUp </a></li>
        <li><a class="button-normal" href="KenoResult.aspx">Keno </a></li>

    </ul>
</asp:Content>