<template>
    <span>
        <div class="spacer-4"></div>
        <h2 class="thin orange-text">Creators</h2>
        <div class="row">
            <div class="col l3 m6 s12" v-for="creatorObj in creators">
                <creator :creator="creatorObj"></creator>
            </div>
        </div>
        <div class="spacer-2"></div>
    </span>
</template>

<script>
    import creator from "./creator/Creator.vue";
    export default {
        components: { creator },
        data: ()=> {
            return {
                creators: [
                    {
                        name: "Artur Grigoryan",
                        image:"~/static/img/creators/artur.jpg",
                    }
                ]
            }
        }
    }
</script>
