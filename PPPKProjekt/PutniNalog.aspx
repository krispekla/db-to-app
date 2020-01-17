<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PutniNalog.aspx.cs" Inherits="PPPKProjekt.PutniNalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2 class="m-3">Putni nalozi</h2>
    <div class="container bg-light mt-3 p-5">
        <div class="row">
            <div class="col-6 mx-auto ">
                <fieldset>

                    <br />

                    <div class="row mt-5">
                        <div class="col-5">
                           <%-- <asp:DropDownList CssClass="mb-3" ID="ddlVehicles" runat="server" AutoPostBack="True"
                                Width="150px" OnSelectedIndexChanged="ddlVehicles_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-2">
                            <asp:Button CssClass="btn btn-dark px-3" ID="btnClear" runat="server" Text="Clear" />
                        </div>
               <%--         <div class="col-2 ml-auto">
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
                        </div>--%>
                    </div>

                </fieldset>
            </div>
        </div>
        <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="14px"
            ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
