<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vozaci.aspx.cs" Inherits="PPPKProjekt.Vozaci" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="m-3">Vozaci</h2>
    <div class="container bg-light mt-3 p-5">
        <div class="row">
            <div class="col-6">
                <fieldset>
                    <legend>Pregled vozaca</legend>

                    <asp:GridView ID="gvDrivers" runat="server"></asp:GridView>


                    <br />

                    <div class="row mt-5">
                        <div class="col-5">
                            <asp:DropDownList CssClass="mb-3" ID="ddlDrivers" runat="server" AutoPostBack="True"
                                Width="150px" OnSelectedIndexChanged="ddlDrivers_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row ">
                        <div class="col-5 ">
                            <span>Ime:</span>
                            <asp:TextBox ID="lbName" runat="server" Enabled="False" />
                        </div>
                        <div class="col-5">
                            <span>Prezime:</span>
                            <asp:TextBox ID="lbLastname" runat="server" Enabled="False" />
                        </div>
                    </div>

                    <div class="row mt-3">
                        <div class="col-5 ">
                            <span>Mobile:</span>
                            <asp:TextBox ID="lbMobile" runat="server" Enabled="False" />
                        </div>
                        <div class="col-5">
                            <span>Driving licence::</span>
                            <asp:TextBox ID="lbDriving" runat="server" Enabled="False" />
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-2">
                            <asp:Button CssClass="btn btn-dark px-3" ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" />
                        </div>
                        <div class="col-2 ml-auto">
                            <asp:Button CssClass="btn btn-primary px-3" ID="btnEditToggle" OnClick="btnEditToggle_Click" runat="server" Text="Edit" />
                        </div>
                        <div class="col-3 ml-3">
                            <asp:Button CssClass="btn btn-primary px-3" ID="btnAddDriver" OnClick="btnAddDriver_Click" runat="server" Text="Add new" Enabled="false"/>
                        </div>
                        <div class="col-2">
                            <asp:Button CssClass="btn btn-primary px-4" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" Enabled="false" />
                        </div>
                    </div>

                </fieldset>
            </div>
            <div class="col-6"></div>
        </div>
        <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="11px"
            ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
