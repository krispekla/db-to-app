<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vozaci.aspx.cs" Inherits="PPPKProjekt.Vozaci" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="m-3">Vozaci</h2>
    <div class="container bg-light mt-3 p-5">
        <div class="row">
            <div class="col-6">
                <fieldset>
                    <legend>Pregled vozaca</legend>

                    <asp:DropDownList CssClass="mb-3" ID="ddlDrivers" runat="server" AutoPostBack="True"
                        Width="150px" OnSelectedIndexChanged="ddlDrivers_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />

                    <div class="row ">
                        <div class="col-5 ">
                            <span>Ime:</span>
                            <asp:TextBox ID="lbName" runat="server" />
                        </div>
                        <div class="col-5">
                            <span>Prezime:</span>
                            <asp:TextBox ID="lbLastname" runat="server" />
                        </div>
                    </div>

                    <div class="row mt-3">
                        <div class="col-5 ">
                            <span>Mobile:</span>
                            <asp:TextBox ID="lbMobile" runat="server" />
                        </div>
                        <div class="col-5">
                            <span>Driving licence::</span>
                            <asp:TextBox ID="lbDriving" runat="server" />
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
