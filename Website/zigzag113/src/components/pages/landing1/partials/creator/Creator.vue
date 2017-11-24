<template>
    <div class="card">
        <div class="card-image waves-effect waves-block waves-light">
            <img class="activator" :src="loadImg()">
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
            <p>Here is some more information about this product that is only revealed once clicked on.</p>
        </div>
    </div>
</template>

<script>
    // import img from '../../../../../assets/creators/artur.jpg';
    export default {
        props: {
            creator: {
                type: Object
            }
        },

        methods: {
            loadImg() {
                // return "";
                return require("../../../../../assets/creators/artur.jpg");
            }
        },

        mounted() {
            // this.img = require(creator.image)
            this.img = require("../../../../../assets/creators/artur.jpg")
            // console.log(require.context("../../../../../assets/creators", false, /\.jpg$/));
        }
    }
</script>
