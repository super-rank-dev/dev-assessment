<template>
    <div id="app">
        <div id="nav">
            <router-link to="/">Home</router-link>
            <router-link to="/about">About</router-link>
            <router-link to="/dashboard">Dashboard</router-link>
            <router-link to="/add-client">Add Client</router-link>
        </div>
        <router-view />
    </div>
</template>

<style lang="scss">
#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}

#nav {
    background-color: #333;
    display: flex;

    a {
        font-weight: bold;
        color: #fff;
        padding: 14px;
        background-color: rgb(95, 95, 95);
        text-decoration: none;
        transition: 0.2s;

        &:hover {
            background-color: rgb(0, 150, 0);
        }

        &:active {
            background-color: #343e9a;
        }

        &.router-link-exact-active {
            background-color: #4d5ce3;
        }
    }
}
</style>