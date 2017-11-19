<template>
    <nav class="light-blue lighten-1" role="navigation">
        <div class="nav-wrapper container">
            <a id="logo-container" href="#" class="brand-logo">
                <img src="../assets/logo.png" width="64" height="64">
                <!-- <span class="thin">ZigZag</span> -->
            </a>
            <ul class="right hide-on-med-and-down">
                <span v-for="link in links">
                    <vlink :link="link"></vlink>
                </span>
            </ul>

            <ul id="nav-mobile" class="side-nav">
                <span v-for="link in links">
                    <vlink :link="link"></vlink>
                </span>
            </ul>
            <a href="#" data-activates="nav-mobile" class="button-collapse"><i class="ion-navicon"></i></a>
        </div>
    </nav>
</template>

<script>
    import vlink from './navbar/Link.vue';

    export default {
        components: { vlink },
        data: () => {
            return {
                links: [
                    {
                        url: '#download',
                        text: 'Download'
                    }
                ]
            }
        }
    }
</script>


<style>
    .thin {
        font-weight: 100 !important;
    }
</style>
