<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="i4DAAB.aspx.cs" Inherits="PPPKProjekt.i4DAAB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="m-3">i4 DAAB</h2>
    <div class="container bg-light mt-3 p-5 shadow">
        <div class="row mb-5">
            <div class="col-4">
                <span>Travel order:</span>
                <asp:DropDownList CssClass="mb-3" ID="ddlTravelOrders" runat="server" OnSelectedIndexChanged="ddlTravelOrders_SelectedIndexChanged" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-4">
                <span>Route:</span>
                <asp:DropDownList CssClass="mb-3" ID="ddlRoutes" OnSelectedIndexChanged="ddlRoutes_SelectedIndexChanged" runat="server" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-5">
                <span class="mr-auto">Start time:</span>
                <asp:TextBox ID="txtStartTime" runat="server" Width="200px" type="date"></asp:TextBox>
            </div>
            <div class="col-5">
                <span class="mr-auto">End time:</span>
                <asp:TextBox ID="txtEndTime" runat="server" Width="200px" type="date"></asp:TextBox>
            </div>

        </div>
        <div class="row mb-2">
            <div class="col-5">
                <span class="mr-auto">Start coordinate:</span>
                <asp:TextBox ID="txtStartCood" runat="server" Width="200px"></asp:TextBox>
            </div>
            <div class="col-5">
                <span class="mr-auto">End coordinate:</span>
                <asp:TextBox ID="txtEndCood" runat="server" Width="200px"></asp:TextBox>
            </div>
        </div>
        <div class="row d-flex flex-column">
            <div class="col-5">
                <span class="mr-auto">Distance(km):</span>
                <input id="txtDistance" type="number" min="1" max="1500" runat="server" />
            </div>
            <div class="col-5 mt-2 mb-2">
                <span class="mr-auto">Average speed(km/h):</span>
                <input id="txtAvgspeed" type="number" min="1" max="200" runat="server" />
            </div>
            <div class="col-5">
                <span class="mr-auto">Fuel consumption(litres):</span>
                <input id="txtFuelConsumption" type="number" min="1" max="100" runat="server" />
            </div>
        </div>
        <div class="row mt-5">
            <div class="col-1">
                <asp:Button CssClass="btn btn-dark px-3" ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" />
            </div>
            <div class="col-1">
                <asp:Button CssClass="btn btn-danger px-3" ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete" />
            </div>

            <div class="col-2 ml-5">
                <asp:Button CssClass="btn btn-primary px-5" ID="btnAddOrder" OnClick="btnAddOrder_Click" runat="server" Text="Add new" />
            </div>
            <div class="col-1 mr-auto">
                <asp:Button CssClass="btn btn-primary px-4" ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Update" />
            </div>
        </div>
        <asp:Label ID="lblInfo" CssClass="mt-3" runat="server" Font-Bold="True" Font-Size="14px"
            ForeColor="Red"></asp:Label>
</asp:Content>
