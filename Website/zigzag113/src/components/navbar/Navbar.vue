<template>
    <nav class="light-blue lighten-1" role="navigation">
        <div class="nav-wrapper container">
            <a id="logo-container" href="#" class="brand-logo">
                <img src="../../assets/logo.png" width="64" height="64">
                <span class="thin hide-on-med-and-down" style="position: relative;bottom: 22px; font-size: 28px;">SHAPELESS</span>
            </a>
            <ul class="right hide-on-med-and-down">
                <span v-for="link in links">
                    <vlink :link="link"></vlink>
                </span>
            </ul>

            <ul id="nav-mobile" class="side-nav">
                <div class="row no-margin">
                    <a href="#" class="col s12 center">
                        <img src="../../assets/logo.png" width="64" height="64">
                    </a>
                </div>

                <div class="row center">
                    <span v-for="link in links">
                        <vlink :link="link"></vlink>
                    </span>
                </div>
            </ul>
            <a href="#" data-activates="nav-mobile" class="button-collapse">
                <i class="mdi mdi-menu"></i>
            </a>
        </div>
    </nav>
</template>

<script>
    import vlink from './partials/Link.vue';
    import links from './links.js';

    export default {
        components: { vlink },
        data: () => {
            return {
                links: links.filter(link => link.nav)
            }
        },

        mounted() {
            $(document).ready(() => {
                $(".button-collapse").sideNav();
            })
        }
    }
</script>
