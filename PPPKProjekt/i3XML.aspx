<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="i3XML.aspx.cs" Inherits="PPPKProjekt.i3XML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="m-3">i3 XML</h2>
    <div class="container bg-light mt-3 p-5">
        <div class="row mb-5">
            <div class="col-4">
                <span>Travel order:</span>
                <asp:DropDownList CssClass="mb-3" ID="ddlTravelOrders" runat="server" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-3">
                <span>Route:</span>
                <asp:DropDownList CssClass="mb-3" ID="ddlRoutes" runat="server" AutoPostBack="True"
                    Width="150px">
                </asp:DropDownList>
            </div>
            <div class="col-5">
                <div class="row">
                    <div class="col-4">
                        <asp:Button CssClass="btn btn-primary px-3" ID="btnCreateXML" OnClick="btnCreateXML_Click" runat="server" Text="Create XML" />
                    </div>
                    <div class="col-4">
                        <asp:Button CssClass="btn btn-primary px-3" ID="btnImportXML" OnClick="btnImportXML_Click" runat="server" Text="Import" />
                    </div>
                    <div class="col-4">
                        <asp:Button CssClass="btn btn-primary px-3" ID="btnExportXML" OnClick="btnExportXML_Click" runat="server" Text="Export" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-5">
                <span class="mr-auto">Start time:</span>
                <asp:TextBox ID="TextBox2" runat="server" Width="200px" type="date"></asp:TextBox>
            </div>
            <div class="col-5">
                <span class="mr-auto">End time:</span>
                <asp:TextBox ID="tbStartingDate" runat="server" Width="200px" type="date"></asp:TextBox>
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
        <asp:Label ID="lblInfo" runat="server" Font-Names="Arial"></asp:Label>

</asp:Content>
