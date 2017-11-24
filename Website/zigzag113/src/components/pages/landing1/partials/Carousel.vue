<template>
    <span>
        <div class="carousel carousel-slider center v-height" data-indicators="true">

            <div class="carousel-fixed-item center">
                <a class="btn waves-effect orange thin">DOWNLOAD</a>
            </div>

            <div class="carousel-item red white-text" href="#one!">
                <h2 class="thin"><small>CREATED </small> BY ZIGZAG</h2>
                <img src="../../../../assets/logo.png" style="height: 300px; width: 300px"/>
            </div>

            <div class="carousel-item amber white-text" href="#two!">
                <h2 class="thin">Experience the crazy world of Shapes</h2>
            </div>

            <div class="carousel-item blue white-text" href="#three!">
                <h2 class="thin">Acquaint yourself with the Super-Shapes</h2>
            </div>

        </div>
    </span>
</template>

<script>
    export default{
        mounted() {
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
</style>
