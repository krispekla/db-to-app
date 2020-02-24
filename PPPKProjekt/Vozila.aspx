<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vozila.aspx.cs" Inherits="PPPKProjekt.Vozila" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="m-3">Vozila</h2>
    <div class="container bg-light mt-3 p-5 shadow">
        <div class="row">
            <div class="col-6">

                <legend>Pregled vozila</legend>
                <asp:GridView ID="gvVehicles" runat="server"></asp:GridView>
            </div>

            <div class="col-6">
                <fieldset>

                    <br />

                    <div class="row mt-5">
                        <div class="col-5">
                            <asp:DropDownList CssClass="mb-3" ID="ddlVehicles" runat="server" AutoPostBack="True"
                                Width="150px" OnSelectedIndexChanged="ddlVehicles_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row ">
                        <div class="col-5 ">
                            <span>Vehicle type:</span>
                            <br />
                            <asp:DropDownList CssClass="px-4" ID="ddlType" runat="server" Enabled="False" />
                        </div>
                        <div class="col-5">
                            <span>Plate:</span>
                            <asp:TextBox ID="lbPlate" runat="server" Enabled="False" />
                        </div>
                    </div>

                    <div class="row mt-3">
                        <div class="col-5 ">
                            <span>Brand:</span>
                            <asp:TextBox ID="lbBrand" runat="server" Enabled="False" />
                        </div>
                        <div class="col-5">
                            <span>Year of production:</span>
                            <input ID="lbYear" type="number" min="1970" max="2021"  runat="server" disabled/>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-5 ">
                            <span>Is available:</span>
                            <asp:CheckBox ID="cbIsAvailable" runat="server" Checked="true"  Enabled="false" />
                        </div>
                        <div class="col-5">
                            <span>Milleage:</span>
                            <asp:TextBox ID="lbMilleage" runat="server" Enabled="False" />
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-2">
                            <asp:Button CssClass="btn btn-dark px-3" ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" />
                        </div>
                        <div class="col-2 ml-auto">
                            <asp:Button CssClass="btn btn-danger px-3" ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete" />
                        </div>
                        <div class="col-2 ml-auto">
                            <asp:Button CssClass="btn btn-primary px-3" ID="btnEditToggle" OnClick="btnEditToggle_Click" runat="server" Text="Edit" />
                        </div>
                        <div class="col-3 ml-3">
                            <asp:Button CssClass="btn btn-primary px-3" ID="btnAddVehicle" OnClick="btnAddVehicle_Click" runat="server" Text="Add new" Enabled="false" />
                        </div>
                        <div class="col-2">
                            <asp:Button CssClass="btn btn-primary px-4" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" Enabled="false" />
                        </div>
                    </div>

                </fieldset>
            </div>
        </div>
        <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px"
            ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
