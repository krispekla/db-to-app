<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EF.aspx.cs" Inherits="PPPKProjekt.EF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="m-3">Vozila, servisi (EF)</h1>
    <div class="container bg-light mt-3 p-5 shadow">
        <h2>Prikaz vozila</h2>
        <br />
        <div class="row mx-auto">
            <div class="col-12">
                <asp:GridView ID="gvVehicles" runat="server"></asp:GridView>
            </div>
        </div>
        <h2>Servisi vozila</h2>

        <div class="row mx-auto mt-4">
            <div class="col-4 ">
                <span class="">Vehicle:</span>
                <asp:DropDownList ID="ddlVehicle" runat="server" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged" CssClass="mb-3" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-3">
                <span class="mr-auto">Service:</span>
                <asp:DropDownList ID="ddlService" runat="server" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" CssClass="mb-3" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-4">
                <span class="">Service item:</span>
                <asp:DropDownList ID="ddlServiceItem" runat="server" OnSelectedIndexChanged="ddlServiceItem_SelectedIndexChanged" CssClass="mb-3" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row mx-auto">
            <div class="col-5">
                <asp:GridView ID="gvServiceItems" runat="server"></asp:GridView>
            </div>
            <div class="col-7 d-flex flex-column">

                <div class="row mt-3">
                    <div class="col-4 ml-1">
                        <span>Service name:</span>
                        <asp:TextBox ID="txtServiceName" runat="server" />
                    </div>
                    <div class="col-3 ml-1">
                        <span>Price:</span>
                        <asp:TextBox ID="txtPrice"  runat="server" />
                    </div>
                </div>
                <div class="row mt-5">

                    <div class="col-2">
                        <asp:Button CssClass="btn btn-dark px-3" ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" />
                    </div>
                    <div class="col-2">
                        <asp:Button CssClass="btn btn-danger px-3" ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete" />
                    </div>
                    <div class="col-2">
                        <asp:Button CssClass="btn btn-primary px-3" ID="btnAddService" OnClick="btnAddService_Click" runat="server" Text="Add new" />
                    </div>
                    <div class="col-2 mr-auto">
                        <asp:Button CssClass="btn btn-primary px-4" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Update" />
                    </div>
                </div>
                <div class="row mt-4">
                     <div class="col-3 mr-auto">
                        <asp:Button CssClass="btn btn-primary px-4" ID="btnAddNewService" OnClick="btnAddNewService_Click" runat="server" Text="Add new Service for vehicle" />
                    </div>
                    <div class="col-4 ml-auto">
                        <asp:Button CssClass="btn btn-dark px-3" ID="btnGenerateHTML" OnClick="btnGenerateHTML_Click"  runat="server" Text="Generate HTML" />
                    </div>
                </div>
            </div>
        </div>


        <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px"
            ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
