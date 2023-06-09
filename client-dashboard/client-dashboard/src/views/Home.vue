<script>
import Welcome from "@/components/Welcome.vue";

export default {
	name: "Home",
	components: {
		Welcome,
	}
};
</script>

<template>
	<div class="home">
		<img alt="Vue logo" src="../assets/logo.png" />
		<Welcome msg="Welcome" />
	</div>
</template>

<style scoped>
.home {
	margin: 200px;
    font-style: italic;
}
</style>