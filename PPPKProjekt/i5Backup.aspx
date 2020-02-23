<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="i5Backup.aspx.cs" Inherits="PPPKProjekt.i5Backup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="m-3">i4 DAAB</h2>
    <div class="container bg-light mt-3 p-5">
        <div class="row mt-3">
            <div class="col-4">
                <asp:Button CssClass="btn btn-primary px-5" ID="btnBackup" OnClick="btnBackup_Click" runat="server" Text="Backup (DB to XML)" />
            </div>
            <div class="col-4">
                <asp:Button CssClass="btn btn-primary px-5" ID="btnCleanDB" OnClick="btnCleanDB_Click" runat="server" Text="Clean DB" />
            </div>
            <div class="col-4">
                <asp:Button CssClass="btn btn-primary px-5" ID="btnRestore" OnClick="btnRestore_Click" runat="server" Text="Restore (XML to DB)" />
            </div>

        </div>
        <asp:Label ID="lblInfo" CssClass="mt-3" runat="server" Font-Bold="True" Font-Size="14px"
            ForeColor="Red"></asp:Label>
</asp:Content>
