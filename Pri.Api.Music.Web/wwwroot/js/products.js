let vue = new Vue({
    el: "#app",
    name: "products",
    data: {
        loginUrl: "https://localhost:7104/api/Auth/Login",
        registerUrl: "https://localhost:7104/api/Auth/Register",
        baseUrl: "https://localhost:7104/api/",
        username: "",
        password: "",
        loading: false,
        showError: false,
        artists: [],
        records: [],
        loggedIn: false,
        errorMessage: "",
        newRecord: {
            title: "",
            price: "",
            image: null,
            genreId: "",
            artistId: "",
            propertyIds: []
        },
        newUser: {
            username: "",
            password: "",
            repeatPassword: "",
            firstname: "",
            lastname: "",
            dateOfBirth: new Date().toLocaleDateString('en-CA'),
        },
        genres: [],
        properties: [],
        token: "",
        newArtist: "",
        selectedArtist: "",
        tokenObject: null,
        profileImage: "",
        emailAdress: "",
        dateOfBirth: new Date().toLocaleDateString('en-CA'),
        isAdmin: false,
        adminProductsVisible: false,
        adminArtistsVisible: false,
    },
    created: function () {
        if (sessionStorage.getItem('token') !== null) {
            this.tokenObject = this.decodeToken(window.sessionStorage.getItem('token'));
            //get the emailadress
            this.emailAdress = this.tokenObject["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
            //get the role and store in sessionStorage
            const role = this.tokenObject["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            if (role === "Admin") {
                this.isAdmin = true;
            }
            this.loggedIn = true;
            this.profileImage = "data:image/png;base64,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";
            if (this.tokenObject["profile-image"] === null) {
                this.profileImage = this.tokenObject["profile-image"];
            }
            this.loggedIn = true;
            this.getRecords();
            this.getArtists();
            this.getGenres();
            this.getProperties();
        }
    },
    methods: {
        showAdminProducts: async function () {
            this.adminProductsVisible = true;
            this.adminArtistsVisible = false;
        },
        showAdminArtists: async function () {
            this.adminArtistsVisible = true;
            this.adminProductsVisible = false;
        },
        getGenres: async function () {
            const url = `${this.baseUrl}genres`;
            this.genres = await axios.get(url)
                .then(response => {
                    console.log(response.data.genres);
                    return response.data.genres;
                })
                .catch(error => {
                    console.log(error);
                });
        },
        getProperties: async function () {
            const url = `${this.baseUrl}properties`;
            this.properties = await axios.get(url)
                .then(response => {
                    console.log(response.data.properties);
                    return response.data.properties;
                })
                .catch(error => {
                    console.log(error);
                })
        },
        getArtists: async function () {
            const url = `${this.baseUrl}artists`;
            this.artists = await axios.get(url)
                .then(response => {
                    console.log(response.data.artists);
                    return response.data.artists;
                })
                .catch(error => {
                    console.log(error);
                })
        },
        getRecords: async function () {
            const url = `${this.baseUrl}records`;
            this.records = await axios.get(url)
                .then(response => {
                    console.log(response.data.records);
                    return response.data.records;
                })
                .catch(error => {
                    console.log(error);
                })
        },
        getFile: function (event) {
            //put the file in the image
            
        },
        
        createArtist: async function () {
            //create the url

            //create the headers => token
            
            //create the data
            
            //do the post
        },
        deleteArtist: async function (id) {
            //confirm delete

            //send the request
            
        },
        createRecord: async function () {
            //validate => moejezelfdoen

            //create formData

            //create the url

            //post the formdata
        },
        
        showEditArtistModal: function (id) {
            //get the selected artist
            this.selectedArtist = this.artists.find(el => el.id === id);
            //show the modal
            this.toggleModal("editArtistModal");
        },
        updateArtist: async function () {
            //build the update endpoint

            //set the data
            
            //config headers => token
            
            //call the api with axios put
            
        },
        toggleModal: function (modalId) {
            $(`#${modalId}`).modal('toggle');
        },
        //auth functions
        submitLogin: async function () {
            this.showError = false;
            const loginDto = {
                "username": this.username,
                "password": this.password
            };
            this.loading = true;
            let token = await axios.post(this.loginUrl, loginDto)
                .then(response => response.data.token)
                .catch(error => {
                    this.showError = true;
                    this.errorMessage = error;
                    console.log(error)
                });
            this.loading = false;
            window.sessionStorage.setItem("token", token);
            this.tokenObject = this.decodeToken(token);
            this.profileImage = "data:image/png;base64,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";
            if (this.tokenObject["profile-image"] === null) {
                this.profileImage = this.tokenObject["profile-image"];
            }
            //get the emailadress
            this.emailAdress = this.tokenObject["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
            //get the role and store in sessionStorage
            const role = this.tokenObject["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            if (role === "Admin") {
                this.isAdmin = true;
            }
            this.loggedIn = true;
            this.getRecords();
            this.getArtists();
            this.getGenres();
            this.getProperties();
        },
        registerUser: async function () {

            //this.dateOfBirth = new Date().toLocaleDateString('en-CA');
        },
        submitLogout: function () {
            this.tokenObject = "";
            window.sessionStorage.clear();
            this.loggedIn = false;
            this.isAdmin = false;
            this.adminArtistsVisible = false;
            this.adminProductsVisible = false;
        },
        decodeToken: function (token) {
            var base64Url = token.split('.')[1];
            var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            return JSON.parse(jsonPayload);
        },
    }
});