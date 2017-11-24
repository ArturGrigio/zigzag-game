<template>
    <div class="card">
        <div class="card-image">
            <img class="activator" :src="loadImg(creator.image)">
            <a class="btn-floating halfway-fab waves-effect waves-light red activator tooltipped" :data-tooltip="'Connect with '+creator.name" data-delay="1">
                <i class="mdi mdi-share-variant right" style="position:relative; right: 2px;"></i>
            </a>
        </div>
        <div class="card-content">
            <span class="card-title grey-text text-darken-4">
                <span class="truncate flow-text" v-text="creator.name"></span>
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

<style>
    .truncate {
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }
</style>
