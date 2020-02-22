<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PutniNalog.aspx.cs" Inherits="PPPKProjekt.PutniNalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="m-3">Putni nalozi</h1>
    <div class="container bg-light mt-3 p-5">
        <h2>Prikaz naloga</h2>
        <br />
        <div class="row mx-auto">
            <div class="col-12">
                <div class="row">
                    <asp:DropDownList ID="ddlFilterOrders" runat="server" CssClass="mb-3" AutoPostBack="True"
                        Width="150px" OnSelectedIndexChanged="ddlFilterOrders_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="row">

                    <asp:GridView ID="gvTransportOrder" runat="server">
                    </asp:GridView>
                </div>
            </div>
        </div>
        <h2 class="mt-5 mb-5">CRUD naloga</h2>
        <div class="row mb-4 ml-3">
            <span>Selected travel order:</span>
                <asp:DropDownList CssClass="mb-3 ml-3 mr-auto" ID="ddlSelectedOrder" runat="server" AutoPostBack="True"
                    Width="150px" OnSelectedIndexChanged="ddlSelectedOrder_SelectedIndexChanged">
                </asp:DropDownList>
        </div>
        <div class="row mx-auto">

            <div class="col-5 ">
                <span class="mr-auto">Order status:</span>
                <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="mb-3" AutoPostBack="True" Enabled="false"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-5">
                <span class="mr-auto">Vehicle:</span>
                <asp:DropDownList ID="ddlVehicle" runat="server" CssClass="mb-3" AutoPostBack="True" Enabled="false"
                    Width="150px">
                </asp:DropDownList>
                <asp:Button CssClass="btn btn-dark px-1" ID="btnOnlyAvailable" OnClick="btnOnlyAvailable_Click" runat="server" Text="Only available" />
            </div>
            <div class="col-5">
                <span class="mr-auto">Driver:</span>
                <asp:DropDownList ID="ddlDriver" runat="server" CssClass="mb-3" AutoPostBack="True" Enabled="false"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-5">
                <span class="mr-auto">Starting City:</span>
                <asp:TextBox ID="lbStartCity" runat="server" Enabled="False" />
            </div>
            <div class="col-5">
                <span class="mr-auto">Finish City:</span>
                <asp:TextBox ID="lbFinishCity" runat="server" Enabled="False" />
            </div>
            <div class="col-5">
                <span class="mr-auto">Total days:</span>
                <input id="lbTotalDays" type="number" min="1" max="100" runat="server" disabled />
            </div>
            <div class="col-5 mt-2">
                <span class="mr-auto">Starting day:</span>
                <asp:TextBox ID="tbStartingDate" runat="server" Width="200px" type="date" Enabled="false"></asp:TextBox>
            </div>

        </div>
        <div class="row mt-5">
            <div class="col-1">
                <asp:Button CssClass="btn btn-dark px-3" ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" />
            </div>
            <div class="col-1">
                <asp:Button CssClass="btn btn-danger px-3" ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete" />
            </div>
            <div class="col-1">
                <asp:Button CssClass="btn btn-primary px-3" ID="btnEditToggle" OnClick="btnEditToggle_Click" runat="server" Text="Edit" />
            </div>
            <div class="col-2 ml-5">
                <asp:Button CssClass="btn btn-primary px-5" ID="btnAddOrder" OnClick="btnAddOrder_Click" runat="server" Text="Add new" Enabled="false" />
            </div>
            <div class="col-1 mr-auto">
                <asp:Button CssClass="btn btn-primary px-4" ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Enabled="false" />
            </div>
        </div>
        <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px"
            ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
