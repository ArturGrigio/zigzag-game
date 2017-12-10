<template>
    <span>
        <div class="carousel carousel-slider center v-height" data-indicators="true">

            <div class="carousel-fixed-item center">
                <a href="/download" class="btn waves-effect orange thin">DOWNLOAD</a>
            </div>

            <div class="carousel-item red white-text" href="#one!">
                <h2 class="thin"><small>CREATED </small> BY ZIGZAG</h2>
                <div class="row">
                    <img id="slide-0" src="../../../../assets/logo.png"s/>
                </div>
            </div>

            <div class="carousel-item blue white-text" href="#one!">
                <h2 class="thin">Easy to Learn, Hard to Master</h2>
                <div class="row">
                    <img id="slide-1" class="l4 offset-l4 m6 offset-m3 s12 slide" src="../../../../assets/misc/slide_1.png"/>
                </div>
            </div>

            <div class="carousel-item yellow" href="#one!">
                <h2 class="thin">Play as 3 Different Shapes</h2>
                <div class="row">
                    <img id="slide-2" class="l4 offset-l4 m6 offset-m3 s12 slide" src="../../../../assets/misc/slide_2.png"/>
                </div>
            </div>

        </div>
    </span>
</template>

<script>
    export default {
        methods: {
            resize() {
                // let wrappers = $('.slide-wrapper')
                // $.map(wrappers, (item, index) => {
                //     console.log($(item).find('img')[0])
                //     console.log($($(item).find('img')[0]).width())
                // })
            }
        },

        mounted() {
            $(window).resize(this.resize())
            $(document).ready(() => {
                $('.carousel.carousel-slider').carousel({fullWidth: true});
            })
        }
    }
</script>

<style>
    .v-height{
        min-height: 500px !important;
        max-height: 500px !important;
    }
    .indicator-item {
        background-color: cyan !important;
    }
    .indicator-item.active {
        background-color: blue !important;
    }
    #slide-0 {
        width: 300px;
        height: 300px;
    }
    #slide-1 {
        width: 750px;
        height: 300px;
    }
    #slide-2 {
        width: 700px;
        height: 300px;
    }
    @media (max-width: 771px) {
        .v-height{
            min-height: 400px !important;
            max-height: 400px !important;
        }
        #slide-0 {
            width: 150px;
            height: 150px;
        }
        #slide-1 {
            width: 375px !important;
            height: 150px !important;
        }
        #slide-2 {
            width: 350px;
            height: 150px;
        }
    }
</style>
