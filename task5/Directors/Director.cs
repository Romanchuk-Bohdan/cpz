using BuilderTask.Interfaces;

namespace BuilderTask.Directors
{
    public class Director
    {
        public void ConstructDreamHero(ICharacterBuilder builder)
        {
            builder.SetName("Артур (Паладин Світла)")
                .SetHeight(190)
                .SetBodyType("Атлетична")
                .SetHairColor("Золотисте")
                .SetEyeColor("Блакитні")
                .SetClothing("Сяючі міфрилові обладунки")
                .AddInventoryItem("Святий меч Екскалібур")
                .AddInventoryItem("Щит Віри")
                .AddDeed("Врятував сирітський притулок від пожежі")
                .AddDeed("Звільнив королівство від прокляття");
        }

        public void ConstructNemesis(ICharacterBuilder builder)
        {
            builder.SetName("Морґот (Володар Тіней)")
                .SetHeight(215)
                .SetBodyType("Масивна, загрозлива")
                .SetHairColor("Попелясте")
                .SetEyeColor("Палаючі червоні")
                .SetClothing("Плащ з темної матерії та обсидіанова броня")
                .AddInventoryItem("Коса Душ")
                .AddInventoryItem("Кільце Темряви")
                .AddDeed("Спалив ельфійські ліси")
                .AddDeed("Викрав стародавній артефакт");
        }
    }
}