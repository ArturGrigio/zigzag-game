<template>
    <div class="card">
        <div class="card-image waves-effect waves-block waves-light">
            <img class="activator" :src="loadImg(creator.image)">
            <!-- <img class="activator" src="../../../../../static/creators/artur.jpg"> -->
            <!-- <img v-lazy="creator.image"/> -->
        </div>
        <div class="card-content">
            <span class="card-title activator grey-text text-darken-4">
                <span v-text="creator.name"></span>
                <i class="mdi mdi-share-variant right"></i>
            </span>
        </div>
        <div class="card-reveal">
            <span class="card-title grey-text text-darken-4">
                <span v-text="creator.name"></span>
                <i class="mdi mdi-close right"></i>
            </span>
            <br />
            <a class="col s12" v-for="link in creator.links" v-bind:href="link.url" v-html="link.text"></a>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            creator: {
                type: Object
            }
        },

        methods: {
            loadImg(img) {
                return require("@/assets/creators/"+ img);
            }
        },
    }
</script>
